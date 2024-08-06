using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data
{
    public class DescrpitionRootObject
    {
        public Description.Tools[] tools;
        public Description.Symptoms[] symptoms;
        public Description.Sicknesses[] sicknesses;
        public Description.Recipes[] recipes;
        public Description.Items[] items;
    }
}
