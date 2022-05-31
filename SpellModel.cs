using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLSSTracker
{
    public class SpellModel
    {
        public string type { get; set; }
        public string version { get; set; }
        public SpellData data { get; set; }
    }
    public class SpellData
    {
        public Summonerbarrier SummonerBarrier { get; set; }
        public Summonerboost SummonerBoost { get; set; }
        public Summonerdot SummonerDot { get; set; }
        public Summonerexhaust SummonerExhaust { get; set; }
        public Summonerflash SummonerFlash { get; set; }
        public Summonerhaste SummonerHaste { get; set; }
        public Summonerheal SummonerHeal { get; set; }
        public Summonermana SummonerMana { get; set; }
        public Summonerpororecall SummonerPoroRecall { get; set; }
        public Summonerporothrow SummonerPoroThrow { get; set; }
        public Summonersmite SummonerSmite { get; set; }
        public Summonersnowurfsnowball_Mark SummonerSnowURFSnowball_Mark { get; set; }
        public Summonersnowball SummonerSnowball { get; set; }
        public Summonerteleport SummonerTeleport { get; set; }
        public Summoner_Ultbookplaceholder Summoner_UltBookPlaceholder { get; set; }
        public Summoner_Ultbooksmiteplaceholder Summoner_UltBookSmitePlaceholder { get; set; }
    }

    public class Summonerbarrier
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues
    {
    }

    public class SpellImage
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerboost
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues1 datavalues { get; set; }
        public float[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage1 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues1
    {
    }

    public class SpellImage1
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerdot
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues2 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage2 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues2
    {
    }

    public class SpellImage2
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerexhaust
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues3 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage3 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues3
    {
    }

    public class SpellImage3
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerflash
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues4 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage4 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues4
    {
    }

    public class SpellImage4
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerhaste
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues5 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage5 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues5
    {
    }

    public class SpellImage5
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerheal
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues6 datavalues { get; set; }
        public float[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage6 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues6
    {
    }

    public class SpellImage6
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonermana
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues7 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage7 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues7
    {
    }

    public class SpellImage7
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerpororecall
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues8 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage8 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues8
    {
    }

    public class SpellImage8
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerporothrow
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues9 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage9 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues9
    {
    }

    public class SpellImage9
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonersmite
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues10 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage10 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues10
    {
    }

    public class SpellImage10
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonersnowurfsnowball_Mark
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues11 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage11 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues11
    {
    }

    public class SpellImage11
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonersnowball
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues12 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage12 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues12
    {
    }

    public class SpellImage12
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summonerteleport
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues13 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage13 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues13
    {
    }

    public class SpellImage13
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summoner_Ultbookplaceholder
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues14 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage14 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues14
    {
    }

    public class SpellImage14
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Summoner_Ultbooksmiteplaceholder
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tooltip { get; set; }
        public int maxrank { get; set; }
        public int[] cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public int[] cost { get; set; }
        public string costBurn { get; set; }
        public Datavalues15 datavalues { get; set; }
        public int[][] effect { get; set; }
        public string[] effectBurn { get; set; }
        public object[] vars { get; set; }
        public string key { get; set; }
        public int summonerLevel { get; set; }
        public string[] modes { get; set; }
        public string costType { get; set; }
        public string maxammo { get; set; }
        public int[] range { get; set; }
        public string rangeBurn { get; set; }
        public SpellImage15 image { get; set; }
        public string resource { get; set; }
    }

    public class Datavalues15
    {
    }

    public class SpellImage15
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

}




