namespace cvti.data.Output
{
    public static class OkruhExtensions
    {
        public static string VratNadpisTransfery(this OkruhZostavyEnum okruh, HlavickaType type)
        {
            switch (type)
            {
                case HlavickaType.Prijmy:
                    return "Príjmy";
                case HlavickaType.Vydavky:
                    return "Výdavky";
                case HlavickaType.Transfery:
                    return "Transfery";
                case HlavickaType.Nezaradene:
                    return "Nezaradene";
                default:
                    break;
            }

            switch (okruh)
            {
                case OkruhZostavyEnum.MŠVVaM:
                    return "Rozpis MŠVVaM";
                case OkruhZostavyEnum.MV:
                    return "Rozpis ministerstva vnútra";
                case OkruhZostavyEnum.MaO:
                    return "Rozpis miest a obcí";
                case OkruhZostavyEnum.VUC:
                    return "Rozpis vyšších územných celkov";
                case OkruhZostavyEnum.CEL:
                    return "Rozpis celkov";
                default:
                    break;
            }

            return string.Empty;
        }
    }
}
