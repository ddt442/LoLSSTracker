using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace LoLSSTracker
{
    public partial class MainForm : Form
    {
        System.Windows.Forms.Timer TypingTimeout = new System.Windows.Forms.Timer();
        private int counter;
        private string summonerName;
        DataProcessor processor = new DataProcessor();

        int[] ss1cd = new int[5];
        int[] ss2cd = new int[5];
        bool InGameFlag = false;
        public MainForm()
        {
            InitializeComponent();
            InitializeTypingTimer();
        }
        private void InitializeTypingTimer()
        {
            counter = 0;
            TypingTimeout.Interval = 1250;
            TypingTimeout.Enabled = true;
            TypingTimeout_Tick(null, null);

            TypingTimeout.Tick += new EventHandler(TypingTimeout_Tick);
        }
        private void TypingTimeout_Tick(object sender, EventArgs e)
        {
            if(counter >= 1)
            {
                TypingTimeout.Enabled=false;
                counter = 0;
            }
            else
            {
                TypingTimeout.Enabled=true;
                counter++;
                UpdateInGameLabel(summonerName);
            }

        }

        private void summonerNameTextbox_TextChanged(object sender, EventArgs e)
        {
            TypingTimeout.Stop();
            TypingTimeout.Start();
            summonerName = summonerNameTextbox.Text;
        }

        private async void UpdateInGameLabel(string summonerName)
        {
            string isInGame = await processor.checkIfSummonerInGame(summonerName);

            PictureBox[] summonerImages = { summoner1, summoner2, summoner3, summoner4, summoner5 };
            PictureBox[] spell1Images = { summoner1spell1, summoner2spell1, summoner3spell1, summoner4spell1, summoner5spell1 };
            PictureBox[] spell2Images = { summoner1spell2, summoner2spell2, summoner3spell2, summoner4spell2, summoner5spell2 };
            Label[] summonerSpell1Cooldowns = { summoner1spell1cooldownLabel, summoner2spell1cooldownLabel, summoner3spell1cooldownLabel, summoner4spell1cooldownLabel, summoner5spell1cooldownLabel };
            Label[] summonerSpell2Cooldowns = { summoner1spell2cooldownLabel, summoner2spell2cooldownLabel, summoner3spell2cooldownLabel, summoner4spell2cooldownLabel, summoner5spell2cooldownLabel };
            PictureBox[] cdrBootsImages = { summoner1cdrBoots, summoner2cdrBoots, summoner3cdrBoots, summoner4cdrBoots, summoner5cdrBoots };
            CheckBox[] cdrCheckBoxes = { summoner1_cdrCheckbox, summoner2_cdrCheckbox, summoner3_cdrCheckbox, summoner4_cdrCheckbox, summoner5_cdrCheckbox };
            InGameCheckerLabel.Text = isInGame;
            if (isInGame == "Not In-Game")
            {
                InGameCheckerLabel.ForeColor = Color.Red;
            }
            else
            {
                InGameFlag = true;
                InGameCheckerLabel.ForeColor = Color.Green;
                List<EnemyPlayer> enemyPlayers = processor.GetEnemyPlayers();
                foreach (EnemyPlayer player in enemyPlayers)
                {
                    if (player != null)
                    {
                        summonerImages[player.playerNumber].LoadAsync(player.champImageURL);
                        spell1Images[player.playerNumber].LoadAsync(player.spell1URL);
                        ss1cd[player.playerNumber] = player.spell1Cooldown;
                        summonerSpell1Cooldowns[player.playerNumber].Text = player.spell1Cooldown.ToString();
                        spell2Images[player.playerNumber].LoadAsync(player.spell2URL);
                        ss2cd[player.playerNumber] = player.spell2Cooldown;
                        summonerSpell2Cooldowns[player.playerNumber].Text = player.spell2Cooldown.ToString();

                    }
                }
                foreach (Label l in summonerSpell1Cooldowns)
                {
                    l.Visible = false;
                }
                foreach (Label l in summonerSpell2Cooldowns)
                {
                    l.Visible = false;
                }
                foreach(PictureBox p in cdrBootsImages)
                {
                    p.Visible = true;
                }
                foreach(CheckBox c in cdrCheckBoxes)
                {
                    c.Visible = true;
                }
                ResetTimers();
                SubscribeCooldownTimers();
                ResetCheckBoxes();
            }
        }

        private void ResetCheckBoxes()
        {
            CheckBox[] cdrCheckBoxes = { summoner1_cdrCheckbox, summoner2_cdrCheckbox, summoner3_cdrCheckbox, summoner4_cdrCheckbox, summoner5_cdrCheckbox };
            foreach (CheckBox c in cdrCheckBoxes)
            {
                if(c.Checked)
                {
                    c.Checked = false;
                }
            }
        }

        private void InGameCheckerLabel_SizeChanged(object sender, EventArgs e)
        {
            InGameCheckerLabel.Left = (this.ClientSize.Width - InGameCheckerLabel.Width) / 2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                c.Visible = false;
            }
            loadingLabel.Visible = true;
            processor.InitModels();
        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            wait(3000);
            PictureBox[] cdrBootsImages = { summoner1cdrBoots, summoner2cdrBoots, summoner3cdrBoots, summoner4cdrBoots, summoner5cdrBoots };
            CheckBox[] cdrCheckBoxes = { summoner1_cdrCheckbox, summoner2_cdrCheckbox, summoner3_cdrCheckbox, summoner4_cdrCheckbox, summoner5_cdrCheckbox };
            foreach (Control c in Controls)
            {
                c.Visible = true;
            }
            foreach(PictureBox p in cdrBootsImages)
            {
                p.Visible = false;
            }
            foreach(CheckBox c in cdrCheckBoxes)
            {
                c.Visible = false;
            }
            loadingLabel.Visible = false;
        }

        private void summonerNameTextbox_Click(object sender, EventArgs e)
        {
            if(summonerNameTextbox.Text == "Enter Summoner Name")
            {
                summonerNameTextbox.Text = "";
            }
        }

        // Cooldowns & Updates
        System.Windows.Forms.Timer s1s1Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s2s1Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s3s1Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s4s1Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s5s1Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s1s2Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s2s2Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s3s2Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s4s2Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer s5s2Timer = new System.Windows.Forms.Timer();

        int s1s1CD, s2s1CD, s3s1CD, s4s1CD, s5s1CD, s1s2CD, s2s2CD, s3s2CD, s4s2CD, s5s2CD;

        private void ResetTimers()
        {
            System.Windows.Forms.Timer[] timers = { s1s1Timer, s2s1Timer, s3s1Timer, s4s1Timer, s5s1Timer, s1s2Timer, s2s2Timer, s3s2Timer, s4s2Timer, s5s2Timer };
            foreach (System.Windows.Forms.Timer timer in timers)
            {
                if(timer.Enabled)
                {
                    timer.Stop();
                }
            }
            UnsubscribeCooldownTimers();
        }

        private void SubscribeCooldownTimers()
        {
            s1s1Timer.Tick += new EventHandler(s1s1Timer_Tick);
            s2s1Timer.Tick += new EventHandler(s2s1Timer_Tick);
            s3s1Timer.Tick += new EventHandler(s3s1Timer_Tick);
            s4s1Timer.Tick += new EventHandler(s4s1Timer_Tick);
            s5s1Timer.Tick += new EventHandler(s5s1Timer_Tick);
            s1s2Timer.Tick += new EventHandler(s1s2Timer_Tick);
            s2s2Timer.Tick += new EventHandler(s2s2Timer_Tick);
            s3s2Timer.Tick += new EventHandler(s3s2Timer_Tick);
            s4s2Timer.Tick += new EventHandler(s4s2Timer_Tick);
            s5s2Timer.Tick += new EventHandler(s5s2Timer_Tick);
        }

        private void UnsubscribeCooldownTimers()
        {
            s1s1Timer.Tick -= s1s1Timer_Tick;
            s2s1Timer.Tick -= s2s1Timer_Tick;
            s3s1Timer.Tick -= s3s1Timer_Tick;
            s4s1Timer.Tick -= s4s1Timer_Tick;
            s5s1Timer.Tick -= s5s1Timer_Tick;
            s1s2Timer.Tick -= s1s2Timer_Tick;
            s2s2Timer.Tick -= s2s2Timer_Tick;
            s3s2Timer.Tick -= s3s2Timer_Tick;
            s4s2Timer.Tick -= s4s2Timer_Tick;
            s5s2Timer.Tick -= s5s2Timer_Tick;
        }

        private void summoner1spell1_Click(object sender, EventArgs e)
        {
            if(InGameFlag)
            {
                if (summoner1spell1cooldownLabel.Text == ss1cd[0].ToString())
                {
                    if(summoner1_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss1cd[0] - 1m) * (100m / 112m);
                        s1s1CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s1s1CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner1spell1cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner1spell1cooldownLabel.Text = s1s1CD.ToString();
                        }
                        summoner1spell1cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s1s1CD = ss1cd[0] - 1;
                    }
                    s1s1Timer.Interval = 1000;
                    s1s1Timer.Enabled = true;
                    s1s1Timer.Start();
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateInGameLabel(summonerName);
        }

        private void s1s1Timer_Tick(object sender, EventArgs e)
        {
            
            if (s1s1CD != 0)
            {
                if(minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s1s1CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner1spell1cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner1spell1cooldownLabel.Text = s1s1CD.ToString();
                }
                summoner1spell1cooldownLabel.Visible = true;
                s1s1CD--;
            }
            else
            {
                TimeSpan t = TimeSpan.FromSeconds(ss1cd[0]);
                string minSeconds = string.Format("{0:D2}:{1:D2}",
                                    t.Minutes,
                                    t.Seconds);
                summoner1spell1cooldownLabel.Text = minSeconds;
                s1s1Timer.Stop();
                s1s1Timer.Enabled=false;
                summoner1spell1cooldownLabel.Visible = false;
            }
        }
        private void summoner2spell1_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner2spell1cooldownLabel.Text == ss1cd[1].ToString())
                {
                    if (summoner2_cdrCheckbox.Checked)
                    {
                        
                        decimal adjustedCooldown = ((decimal)ss1cd[1] - 1m) * (100m / 112m);
                        s2s1CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s2s1CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner2spell1cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner2spell1cooldownLabel.Text = s2s1CD.ToString();
                        }
                        summoner2spell1cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s2s1CD = ss1cd[1] - 1;
                    }
                    s2s1Timer.Interval = 1000;
                    s2s1Timer.Enabled = true;
                    s2s1Timer.Start();
                }
            }
        }

        private void s2s1Timer_Tick(object sender, EventArgs e)
        {

            if (s2s1CD != 0)
            {
                if(minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s2s1CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner2spell1cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner2spell1cooldownLabel.Text = s2s1CD.ToString();
                }
                summoner2spell1cooldownLabel.Visible = true;
                s2s1CD--;
                
            }
            else
            {
                summoner2spell1cooldownLabel.Text = ss1cd[1].ToString();
                s2s1Timer.Stop();
                s2s1Timer.Enabled = false;
                summoner2spell1cooldownLabel.Visible = false;
            }
        }

        private void summoner3spell1_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner3spell1cooldownLabel.Text == ss1cd[2].ToString())
                {
                    if (summoner3_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss1cd[2] - 1m) * (100m / 112m);
                        s3s1CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s3s1CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner3spell1cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner3spell1cooldownLabel.Text = s3s1CD.ToString();
                        }
                        summoner3spell1cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s3s1CD = ss1cd[2] - 1;
                    }
                    s3s1Timer.Interval = 1000;
                    s3s1Timer.Enabled = true;
                    s3s1Timer.Start();
                    
                }
            }
        }

        private void s3s1Timer_Tick(object sender, EventArgs e)
        {

            if (s3s1CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s3s1CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner3spell1cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner3spell1cooldownLabel.Text = s3s1CD.ToString();
                }
                summoner3spell1cooldownLabel.Visible = true;
                s3s1CD--;
            }
            else
            {
                summoner3spell1cooldownLabel.Text = ss1cd[2].ToString();
                s3s1Timer.Stop();
                s3s1Timer.Enabled = false;
                summoner3spell1cooldownLabel.Visible = false;
            }
        }

        private void summoner4spell1_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner4spell1cooldownLabel.Text == ss1cd[3].ToString())
                {
                    if (summoner4_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss1cd[3] - 1m) * (100m / 112m);
                        s4s1CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s4s1CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner4spell1cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner4spell1cooldownLabel.Text = s4s1CD.ToString();
                        }
                        summoner4spell1cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s4s1CD = ss1cd[3] - 1;
                    }
                    s4s1Timer.Interval = 1000;
                    s4s1Timer.Enabled = true;
                    s4s1Timer.Start();
                }
            }
        }

        private void s4s1Timer_Tick(object sender, EventArgs e)
        {

            if (s4s1CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s4s1CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner4spell1cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner4spell1cooldownLabel.Text = s4s1CD.ToString();
                }
                summoner4spell1cooldownLabel.Visible = true;
                s4s1CD--;
            }
            else
            {
                summoner4spell1cooldownLabel.Text = ss1cd[3].ToString();
                s4s1Timer.Stop();
                s4s1Timer.Enabled = false;
                summoner4spell1cooldownLabel.Visible = false;
            }
        }

        private void summoner5spell1_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner5spell1cooldownLabel.Text == ss1cd[4].ToString())
                {
                    if (summoner5_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss1cd[4] - 1m) * (100m / 112m);
                        s5s1CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s5s1CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner5spell1cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner5spell1cooldownLabel.Text = s5s1CD.ToString();
                        }
                        summoner5spell1cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s5s1CD = ss1cd[4] - 1;
                    }
                    s5s1Timer.Interval = 1000;
                    s5s1Timer.Enabled = true;
                    s5s1Timer.Start();
                    
                }
            }
        }

        private void s5s1Timer_Tick(object sender, EventArgs e)
        {

            if (s5s1CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s5s1CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner5spell1cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner5spell1cooldownLabel.Text = s5s1CD.ToString();
                }
                summoner5spell1cooldownLabel.Visible = true;
                s5s1CD--;
            }
            else
            {
                summoner5spell1cooldownLabel.Text = ss1cd[4].ToString();
                s5s1Timer.Stop();
                s5s1Timer.Enabled = false;
                summoner5spell1cooldownLabel.Visible = false;
            }
        }

        private void summoner1spell2_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner1spell2cooldownLabel.Text == ss2cd[0].ToString())
                {
                    if (summoner1_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss2cd[0] - 1m) * (100m / 112m);
                        s1s2CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s1s2CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner1spell2cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner1spell2cooldownLabel.Text = s1s2CD.ToString();
                        }
                        summoner1spell2cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s1s2CD = ss2cd[0] - 1;
                    }
                    s1s2Timer.Interval = 1000;
                    s1s2Timer.Enabled = true;
                    s1s2Timer.Start();
                    
                }
            }
        }

        private void APIKEYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the updated API Key", "Update API Key", "API_KEY", 0, 0);
            if(!string.IsNullOrEmpty(input))
            {
                processor.APIKey = input;
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void s1s2Timer_Tick(object sender, EventArgs e)
        {

            if (s1s2CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s1s2CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner1spell2cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner1spell2cooldownLabel.Text = s1s2CD.ToString();
                }
                summoner1spell2cooldownLabel.Visible = true;
                s1s2CD--;
            }
            else
            {
                summoner1spell2cooldownLabel.Text = ss2cd[0].ToString();
                s1s2Timer.Stop();
                s1s2Timer.Enabled = false;
                summoner1spell2cooldownLabel.Visible = false;
            }
        }

        private void summoner2spell2_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner2spell2cooldownLabel.Text == ss2cd[1].ToString())
                {
                    if (summoner2_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss2cd[1] - 1m) * (100m / 112m);
                        s2s2CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s2s2CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner2spell2cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner2spell2cooldownLabel.Text = s2s2CD.ToString();
                        }
                        summoner2spell2cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s2s2CD = ss2cd[1] - 1;
                    }
                    s2s2Timer.Interval = 1000;
                    s2s2Timer.Enabled = true;
                    s2s2Timer.Start();
                    summoner2spell2cooldownLabel.Visible = true;
                }
            }
        }

        private void s2s2Timer_Tick(object sender, EventArgs e)
        {

            if (s2s2CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s2s2CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner2spell2cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner2spell2cooldownLabel.Text = s2s2CD.ToString();
                }
                summoner2spell2cooldownLabel.Visible = true;
                s2s2CD--;
            }
            else
            {
                summoner2spell2cooldownLabel.Text = ss2cd[1].ToString();
                s2s2Timer.Stop();
                s2s2Timer.Enabled = false;
                summoner2spell2cooldownLabel.Visible = false;
            }
        }

        private void summoner3spell2_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner3spell2cooldownLabel.Text == ss2cd[2].ToString())
                {
                    if (summoner3_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss2cd[2] - 1m) * (100m / 112m);
                        s3s2CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s3s2CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner3spell2cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner3spell2cooldownLabel.Text = s3s2CD.ToString();
                        }
                        summoner3spell2cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s3s2CD = ss2cd[2] - 1;
                    }
                    s3s2Timer.Interval = 1000;
                    s3s2Timer.Enabled = true;
                    s3s2Timer.Start();
                    summoner3spell2cooldownLabel.Visible = true;
                }
            }
        }
        private void s3s2Timer_Tick(object sender, EventArgs e)
        {

            if (s3s2CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s3s2CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner3spell2cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner3spell2cooldownLabel.Text = s3s2CD.ToString();
                }
                summoner3spell2cooldownLabel.Visible = true;
                s3s2CD--;
            }
            else
            {
                summoner3spell2cooldownLabel.Text = ss2cd[2].ToString();
                s3s2Timer.Stop();
                s3s2Timer.Enabled = false;
                summoner3spell2cooldownLabel.Visible = false;
            }
        }

        private void summoner4spell2_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner4spell2cooldownLabel.Text == ss2cd[3].ToString())
                {
                    if (summoner4_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss2cd[3] - 1m) * (100m / 112m);
                        s4s2CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s4s2CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner4spell2cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner4spell2cooldownLabel.Text = s4s2CD.ToString();
                        }
                        summoner4spell2cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s4s2CD = ss2cd[3] - 1;
                    }
                    s4s2Timer.Interval = 1000;
                    s4s2Timer.Enabled = true;
                    s4s2Timer.Start();
                    summoner4spell2cooldownLabel.Visible = true;
                }
            }
        }

        private void s4s2Timer_Tick(object sender, EventArgs e)
        {

            if (s4s2CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s4s2CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner4spell2cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner4spell2cooldownLabel.Text = s4s2CD.ToString();
                }
                summoner4spell2cooldownLabel.Visible = true;
                s4s2CD--;
            }
            else
            {
                summoner4spell2cooldownLabel.Text = ss2cd[3].ToString();
                s4s2Timer.Stop();
                s4s2Timer.Enabled = false;
                summoner4spell2cooldownLabel.Visible = false;
            }
        }

        private void summoner5spell2_Click(object sender, EventArgs e)
        {
            if (InGameFlag)
            {
                if (summoner5spell2cooldownLabel.Text == ss2cd[4].ToString())
                {
                    if (summoner5_cdrCheckbox.Checked)
                    {
                        decimal adjustedCooldown = ((decimal)ss2cd[4] - 1m) * (100m / 112m);
                        s5s2CD = (int)adjustedCooldown;
                        if (minSecCheckbox.Checked)
                        {
                            TimeSpan t = TimeSpan.FromSeconds(s5s2CD);
                            string minSeconds = string.Format("{0:D2}:{1:D2}",
                                                t.Minutes,
                                                t.Seconds);
                            summoner5spell2cooldownLabel.Text = minSeconds;
                        }
                        else
                        {
                            summoner5spell2cooldownLabel.Text = s5s2CD.ToString();
                        }
                        summoner5spell2cooldownLabel.Visible = true;
                    }
                    else
                    {
                        s5s2CD = ss2cd[4] - 1;
                    }
                    s5s2Timer.Interval = 1000;
                    s5s2Timer.Enabled = true;
                    s5s2Timer.Start();
                    summoner5spell2cooldownLabel.Visible = true;
                }
            }
        }

        private void s5s2Timer_Tick(object sender, EventArgs e)
        {

            if (s5s2CD != 0)
            {
                if (minSecCheckbox.Checked)
                {
                    TimeSpan t = TimeSpan.FromSeconds(s5s2CD);
                    string minSeconds = string.Format("{0:D2}:{1:D2}",
                                        t.Minutes,
                                        t.Seconds);
                    summoner5spell2cooldownLabel.Text = minSeconds;
                }
                else
                {
                    summoner5spell2cooldownLabel.Text = s5s2CD.ToString();
                }
                summoner5spell2cooldownLabel.Visible = true;
                s5s2CD--;
            }
            else
            {
                summoner5spell2cooldownLabel.Text = ss2cd[4].ToString();
                s5s2Timer.Stop();
                s5s2Timer.Enabled = false;
                summoner5spell2cooldownLabel.Visible = false;
            }
        }
    }
}
