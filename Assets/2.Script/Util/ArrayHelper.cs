using System.Collections;

public class ArrayHelper 
{
    public static T[] Add<T>(T p_value, T[] p_array)
    {
        ArrayList t_tempList = new ArrayList();

        foreach (T t_item in p_array) t_tempList.Add(t_item);
        t_tempList.Add(p_value);

        return t_tempList.ToArray(typeof(T)) as T[];
    }

    public static T[] Remove<T>(int p_index, T[] p_array)
    {
        ArrayList t_tempList = new ArrayList();

        foreach (T t_item in p_array) t_tempList.Add(t_item);
        t_tempList.Remove(p_index);

        return t_tempList.ToArray(typeof(T)) as T[];
    }
}
