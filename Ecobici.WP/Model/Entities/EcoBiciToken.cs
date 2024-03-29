﻿namespace Ecobici.WP.Model.Entities
{
    public class EcoBiciToken
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string refresh_token { get; set; }

        public object scope { get; set; }

        public string token_type { get; set; }
    }
}