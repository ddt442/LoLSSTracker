using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Collections;
using Amazon.SecretsManager;
using System.IO;
using Amazon;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json.Linq;

namespace LoLSSTracker
{
    public class DataProcessor
    {
        string ChampionDataURL = "http://ddragon.leagueoflegends.com/cdn/12.10.1/data/en_US/champion.json";
        string SummonerSpellDataURL = "http://ddragon.leagueoflegends.com/cdn/12.10.1/data/en_US/summoner.json";
        string SpellImagesURL = "http://ddragon.leagueoflegends.com/cdn/12.10.1/img/spell/";
        string ChampionImagesURL = "http://ddragon.leagueoflegends.com/cdn/12.10.1/img/champion/";
        public string APIKey = GetSecret();

        public void updateAPIKey()
        {
            APIKey = GetSecret();
        }

        ChampionModel championModel = new ChampionModel();
        SpellModel spellModel = new SpellModel();
        SpellData spellData = new SpellData();

        List<EnemyPlayer> enemyPlayers = new List<EnemyPlayer>();


        public static string GetSecret()
        {
            string secretName = "arn:aws:secretsmanager:us-east-2:654417133007:secret:LoLSSTracker_API_Key-wWmTfw";
            string region = "us-east-2";
            string secret = "";

            MemoryStream memoryStream = new MemoryStream();

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretName;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

            GetSecretValueResponse response = null;

            // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
            // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            // We rethrow the exception by default.

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                // Deal with the exception here, and/or rethrow at your discretion
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (System.AggregateException ae)
            {
                // More than one of the above exceptions were triggered.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }

            // Decrypts secret using the associated KMS key.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                secret = response.SecretString;
            }
            else
            {
                memoryStream = response.SecretBinary;
                StreamReader reader = new StreamReader(memoryStream);
                string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            }
            JObject jsonSecret = JObject.Parse(secret);
            string secretKey = (string)jsonSecret["APIKey"];
            return secretKey;
        }

        public List<EnemyPlayer> GetEnemyPlayers()
        {
            return enemyPlayers;
        }
        public async void InitModels()
        {
            championModel = await requestChampionData();
            spellModel = await requestSummonerSpellData();
            spellData = spellModel.data;
        }
        public async Task<SummonerModel> requestEncrpytedSummonerID(string region, string summonerName)
        {
            string url = "";
            if(string.IsNullOrEmpty(region) || string.IsNullOrEmpty(summonerName))
            {
                url = "";
            }
            else
            {
                url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}?api_key={APIKey}";
            }

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    SummonerModel summoner = await response.Content.ReadAsAsync<SummonerModel>();
                    return summoner;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<LiveGameModel> requestLiveGameData(string region, string summonerName)
        {
            SummonerModel user = await requestEncrpytedSummonerID(region, summonerName);
            string summonerEncryptedID = user.id;
            string url = "";
            if (string.IsNullOrEmpty(region) || string.IsNullOrEmpty(summonerName))
            {
                url = "";
            }
            else
            {
                url = $"https://{region}.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{summonerEncryptedID}?api_key={APIKey}";
            }

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    LiveGameModel liveGame = await response.Content.ReadAsAsync<LiveGameModel>();
                    return liveGame;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<ChampionModel> requestChampionData()
        {
            string url = ChampionDataURL;
            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    ChampionModel champions = await response.Content.ReadAsAsync<ChampionModel>();
                    return champions;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public string lookupChampionImage(string championId)
        {
            string foundChampURL = "";
            var champDict = championModel.data;
            champDict.ToList().ForEach(champion =>
            {
                if(champion.Value.key == championId)
                {
                    foundChampURL = ChampionImagesURL + champion.Value.id + ".png";
                }
            });

            return foundChampURL;
        }
        public async Task<SpellModel> requestSummonerSpellData()
        {
            string url = SummonerSpellDataURL;
            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    SpellModel spells = await response.Content.ReadAsAsync<SpellModel>();
                    return spells;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public SummonerSpell lookupSummonerSpellData(string spellId)
        {
            Object[] spellList = new object[] {spellData.SummonerBarrier, spellData.SummonerBoost, spellData.SummonerDot, spellData.SummonerExhaust,
                spellData.SummonerFlash, spellData.SummonerHaste, spellData.SummonerHeal, spellData.SummonerMana, spellData.SummonerPoroRecall,
                spellData.SummonerPoroThrow, spellData.SummonerSmite, spellData.SummonerSnowball, spellData.SummonerSnowURFSnowball_Mark, spellData.SummonerTeleport,
                spellData.Summoner_UltBookPlaceholder, spellData.Summoner_UltBookSmitePlaceholder};

            SummonerSpell summonerSpell = new SummonerSpell();
            int correctCooldown = 0;
            int currentCooldown = 0;
            string imageName = "";
            bool foundSpell = false;
            foreach (Object spell in spellList)
            {
                PropertyInfo[] spellProperties;
                spellProperties = spell.GetType().GetProperties();
                foreach (PropertyInfo property in spellProperties)
                {
                    if (property.Name == "cooldownBurn")
                    {
                        currentCooldown = Int32.Parse(property.GetValue(spell).ToString());
                    }
                    if (property.Name == "key")
                    {
                        if(property.GetValue(spell).ToString() == spellId)
                        {
                            foundSpell = true;
                            correctCooldown = currentCooldown;
                        }
                    }
                    if(property.Name == "image")
                    {
                        if(foundSpell)
                        {
                            imageName = getImageName(property.GetValue(spell));
                            foundSpell = false;
                        }
                    }
                }
            }

            summonerSpell.cooldown = correctCooldown;
            summonerSpell.imageName = imageName;
            summonerSpell.imageURL = SpellImagesURL + summonerSpell.imageName;
            return summonerSpell;
        }

        public string getImageName(object obj)
        {
            var type = obj.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                object value = propertyInfo.GetValue(obj, null);
                if (propertyInfo.PropertyType.IsGenericType &&
                    propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    foreach (object o in (IEnumerable)value)
                    {
                        getImageName(o);
                    }
                }
                else
                {
                    if (value.ToString().Contains("Summoner"))
                    {
                        return value.ToString();
                    }
                }
            }
            return "";
        }

        public async Task<bool> parseLiveGameData(string region, string summonerName)
        {
            int userTeamId = 0;
            int enemyTeamId = 0;
            int count = 0;
         
            try
            {
                LiveGameModel liveGame = await requestLiveGameData(region, summonerName);
                foreach (Participant p in liveGame.participants)
                {
                    if(p.summonerName == summonerName)
                    {
                        userTeamId = p.teamId;
                        if(userTeamId == 100)
                        {
                            enemyTeamId = 200;
                        }
                        else
                        {
                            enemyTeamId = 100;
                        }
                        break;
                    }
                }
                foreach (Participant p in liveGame.participants)
                {
                    if(p.teamId == enemyTeamId)
                    {
                        EnemyPlayer enemy = new EnemyPlayer();
                        SummonerSpell spell1 = lookupSummonerSpellData(p.spell1Id.ToString());
                        SummonerSpell spell2 = lookupSummonerSpellData(p.spell2Id.ToString());
                        string champion = lookupChampionImage(p.championId.ToString());
                        //Console.WriteLine(champion + " | Spell1: " + spell1.imageName + " | Spell2: " + spell2.imageName);
                        enemy.champImageURL = champion;
                        enemy.spell1URL = spell1.imageURL;
                        enemy.spell1Cooldown = spell1.cooldown;
                        enemy.spell2URL = spell2.imageURL;
                        enemy.spell2Cooldown = spell2.cooldown;
                        enemy.playerNumber = count;
                        enemyPlayers.Add(enemy);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void DisplayObject(object obj)
        {
            var type = obj.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                object value = propertyInfo.GetValue(obj, null);
                if (propertyInfo.PropertyType.IsGenericType &&
                    propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    foreach (object o in (IEnumerable)value)
                    {
                        DisplayObject(o);
                    }
                }
                else
                {
                    Console.WriteLine(value);
                }
            }
        }

        public async Task<string> checkIfSummonerInGame(string summonerName)
        {
            if(await parseLiveGameData("NA1", summonerName))
            {
                return "In-Game";
            }
            return "Not In-Game";
        }
    }
}
