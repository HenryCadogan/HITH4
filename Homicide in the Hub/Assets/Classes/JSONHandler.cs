using System.Collections.Generic;

namespace Assets.Classes
{
    public class JSONHandler
        //CLASS ADDITION BY WEDUNNIT
    {
        private static List<string> tempList = new List<string>();
        //access data (and print it)
        public static  List<string> AccessData(JSONObject obj,string data)
        {
            tempList.Clear();
            foreach (var line in obj.GetField(data).list)
            {
                tempList.Add(line.str);
            }
            return tempList;
        }
    }
}

