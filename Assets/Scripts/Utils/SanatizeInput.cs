using System.Collections;
using System.Collections.Generic;

public static class SanatizeInput
{
    public static Dictionary<string, string[]> Input(string InputString, string SplitBy = " ")
    {
        Dictionary<string,string[]> parameters = new Dictionary<string, string[]>();
        string name; // was used for debugging
        if(InputString.Contains(SplitBy))
        {
            parameters.Add(name = InputString.Split(SplitBy)[0],InputString.Split(SplitBy)[1..]); // array[Range.StartAt(1)] 
        }
        else
        {
            parameters.Add(name = InputString,new string[0]);
        }
        return parameters;
    }
}
