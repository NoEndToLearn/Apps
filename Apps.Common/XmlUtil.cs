
using System;
using System.IO;
using System.Xml.Serialization;

/// <summary>
///  Xml序列化与反序列化
/// </summary>
/// <typeparam name="T"></typeparam>
public class XmlUtil<T>
    where T : class, new()
{
    #region 反序列化

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="xml">XML字符串</param>
    /// <returns>T</returns>
    public static T Deserialize(string xml)
    {
        try
        {
            T t = default(T);
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                object obj = xmldes.Deserialize(sr);
                if (obj is T)
                {
                    return (T)obj;
                }
                else
                {
                    return default(T);
                }
            }
        }
        catch (Exception e)
        {
            return default(T);
        }
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="stream"></param>
    /// <returns>T</returns>
    public static T Deserialize(Stream stream)
    {
        XmlSerializer xmldes = new XmlSerializer(typeof(T));
        object obj = xmldes.Deserialize(stream);
        return obj is T ? (T)obj : default(T);
    }
    #endregion

    #region 序列化

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>string</returns>
    public static string Serializer(object obj)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            try
            {
                // 序列化对象
                xml.Serialize(stream, obj);
            }
            catch (InvalidOperationException)
            {
                return string.Empty;
            }

            stream.Position = 0;
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }

    #endregion
}