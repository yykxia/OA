using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WX2HK
{
    public class AdressInfo
    {
        public class requestResult
        {
            public int status { get; set; }
            public string message { get; set; }
            public string request_id { get; set; }
            public Result result { get; set; }
        }

        public class Result
        {
            public Location location { get; set; }
            public string address { get; set; }
            public Formatted_Addresses formatted_addresses { get; set; }
            public Address_Component address_component { get; set; }
            public Ad_Info ad_info { get; set; }
            public Address_Reference address_reference { get; set; }
            public int poi_count { get; set; }
            public Pois[] pois { get; set; }
        }

        public class Location
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Formatted_Addresses
        {
            public string recommend { get; set; }
            public string rough { get; set; }
        }

        public class Address_Component
        {
            public string nation { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string district { get; set; }
            public string street { get; set; }
            public string street_number { get; set; }
        }

        public class Ad_Info
        {
            public string adcode { get; set; }
            public string name { get; set; }
            public Location1 location { get; set; }
            public string nation { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string district { get; set; }
        }

        public class Location1
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Address_Reference
        {
            public Village village { get; set; }
            public Town town { get; set; }
        }

        public class Village
        {
            public string title { get; set; }
            public Location2 location { get; set; }
            public float _distance { get; set; }
            public string _dir_desc { get; set; }
        }

        public class Location2
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Town
        {
            public string title { get; set; }
            public Location3 location { get; set; }
            public int _distance { get; set; }
            public string _dir_desc { get; set; }
        }

        public class Location3
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Pois
        {
            public string id { get; set; }
            public string title { get; set; }
            public string address { get; set; }
            public string category { get; set; }
            public Location4 location { get; set; }
            public Ad_Info1 ad_info { get; set; }
            public float _distance { get; set; }
            public string _dir_desc { get; set; }
        }

        public class Location4
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Ad_Info1
        {
            public string adcode { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string district { get; set; }
        }

    }
}