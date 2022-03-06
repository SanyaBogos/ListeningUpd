using Microsoft.EntityFrameworkCore.Migrations;

namespace Listening.Web.Migrations
{
    public partial class InsertVideoNamesToTimeCodes : Migration
    {
        private readonly string[] _videoNames = new string[] { "Alumminium", "Bitum", "Cherepiza", "CSP", "Dekor", "DerevianieOkna", "Derevo",
            "Electrica", "FinishSloy", "Glina", "GlinaIIzvest", "Isvest", "KakieOknaVybrat", "Kirpich", "Lenta", "Mauerlat",
            "Metalocherepitsa", "MetaloPlastik", "MezhetazhnoePerekritie", "NoWorry", "Oblizovka", "Ondulin", "Otoplenie",
            "Plita", "Podokonniki", "PodshivkaPotolka", "PoiskBrigady", "PoiskIZakupkaStroiMater", "PokrytiePola", "Razmetka",
            "Saman", "SamanPeregorodki1", "SamanPeregorodki2", "SamanPeregorodki3", "Shifer", "Soleco", "SostavlenieDogovora",
            "StenoviePaneli", "StroiInstrum", "Svai", "TeplieSteny", "TipyCrovel", "TrosnikIDranka", "UstroistvoKrovli",
            "VibrirovanieBetona", "VodoprovodIKanal", "ZakladCommunicazii",

            "GidInstruments", "KakUteplit", "Otoplenie", "SixTehnologiiStroitelstva", "SproecttirovatDomZa3", "TwentyRecomendations"
        };


        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "TimeCode_Videos", column: "Name", values: _videoNames);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "TimeCode_Videos", keyColumn: "Name", keyValues: _videoNames);
        }
    }
}
