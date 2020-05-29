using System.ComponentModel;

namespace HttpUtility.EndPoints.ShippingService.Enums
{
    public enum RateShopperShipperCodesEnum
    {
        [Description("UPS GROUND COMMERCIAL")]
        S01,
        [Description("UPS NEXT DAY AIR COMMERCIAL")]
        S02,
        [Description("UPS 2ND DAY AIR COMMERCIAL")]
        S03,
        [Description("PARCEL POST")]
        S04,
        [Description("BEST WAY")]
        S10,
        [Description("UPS 2ND DAY - AM DELIVERY")]
        S11,
        [Description("UPS SATURDAY DELIVERY NEXT DAY")]
        S12,
        [Description("UPS SECOND DAY AIR - SATURDAY")]
        S13,
        [Description("UPS AIRSAVER COMMERCIAL / 3PM")]
        S14,
        [Description("UPS 3 DAY AIR COMMERCIAL")]
        S15,
        [Description("UPS NEXTDAY ERLY AM COMMERCIAL")]
        S20,
        [Description("UPS NXTDAY SAT EARLY AM COMM")]
        S21,
        [Description("UPS GROUND CANADA")]
        S25,
        [Description("UPS GROUND RESIDENTIAL")]
        S41,
        [Description("UPS NEXT DAY AIR RESIDENTIAL")]
        S42,
        [Description("UPS 2ND DAY AIR RESIDENTIAL")]
        S43,
        [Description("UPS AIRSAVER RESIDENTIAL / 3PM")]
        S44,
        [Description("UPS 3 DAY AIR RESIDENTIAL")]
        S45,
        [Description("UPS GROUND (CUSTOM LITERATURE)")]
        S81,
        [Description("UPS NEXT DAY (CUSTOM LIT)")]
        S82,
        [Description("UPS 2ND DAY AIR (CUSTOM LIT)")]
        S83,
        [Description("UPS 3RD DAY AIR (CUSTOM LIT)")]
        S85,
        [Description("FedEx GROUND")]
        SF1,
        [Description("FedEx STANDARD OVERRIGHT")]
        SF2,
        [Description("FedEx 2ND DAY")]
        SF3,
        [Description("FedEx EXPRESS SAVER")]
        SF5,
        [Description("FedEx FIRST OVERRIGHT")]
        SF6,
        [Description("FedEx PRIORITY OVERRIGHT")]
        SF7,
        [Description("NO RATE SHOPPER")]
        NRS,
        [Description("INVALID SERVICE CODE")]
        S33,

        //UPSNRI / UPSCANADA
        [Description("UPS Standard CANADA")]
        S38,
        [Description("UPS Expedited CANADA")]
        S46,
        [Description("UPS Saver CANADA")]
        S48,
        [Description("UPS Express CANADA")]
        S39,
        [Description("UPS Express Plus CANADA")]
        S47,

        //UPSINTERNATIONAL
        [Description("UPS Worldwide Expedited")]
        S250,
        [Description("UPS Worldwide Saver")]
        S251,
        [Description("UPS Worldwide Express")]
        S252,
        [Description("UPS Worldwide Express Plus")]
        S253,
    }
}
