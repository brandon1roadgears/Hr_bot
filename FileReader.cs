using System.Formats.Asn1;
using System.Xml.Linq;

internal static class FIleReader
{
    static private string mainDestination = "D:/Tusur/Work_Bot/UserData";
    
    internal static bool IsUserExist(string userId)
    {
        string path = mainDestination + "/" +  userId;
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Console.WriteLine("NO");
            return false;
        }
        Console.WriteLine("Yes");
        return true;
    }
    internal static bool IsUersRegistrated(string userId)
    {
        return File.Exists(mainDestination + "/" + userId + "/UserNumber.txt");
    }
    internal static void SignUpUser(string userId)
    {
        DirectoryInfo dir = new DirectoryInfo(mainDestination);
        dir.CreateSubdirectory(userId);
    }
    internal static void SaveStep(string userId, int step)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserStep.txt", false))
        {
            writer.Write(step);
            writer.Close();
        }
    }
    internal static int LoadStep(string userId)
    {
        int step;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserStep.txt"))
        {
            step = int.Parse(reader.ReadLine());
            reader.Close();
        }
        return step;
    }

    internal static void SaveName(string userId, string name)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserName.txt", false))
        {
            writer.Write(name);
            writer.Close();
        }
    }
    internal static string LoadName(string userId)
    {
        string name;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserName.txt"))
        {
            name = reader.ReadLine();
            reader.Close();
        }
        return name;
    }
    internal static void SaveRole(string userId, string role)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserRole.txt", false))
        {
            writer.Write(role);
            writer.Close();
        }
    }
    internal static string LoadRole(string userId)
    {
        string role;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserRole.txt"))
        {
            role = reader.ReadLine();
            reader.Close();
        }
        return role;
    }

    internal static void SaveCity(string userId, string city)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserCity.txt", false))
        {
            writer.Write(city);
            writer.Close();
        }
    }
    internal static string LoadCity(string userId)
    {
        string city;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserCity.txt"))
        {
            city = reader.ReadLine();
            reader.Close();
        }
        return city;
    }

    internal static void SavePart(string userId, string part)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserPart.txt", false))
        {
            writer.Write(part);
            writer.Close();
        }
    }
    internal static string LoadPart(string userId)
    {
        string part;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserPart.txt"))
        {
            part = reader.ReadLine();
            reader.Close();
        }
        return part;
    }

    internal static void SavePost(string userId, string post)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserPost.txt", false))
        {
            writer.Write(post);
            writer.Close();
        }
    }
    internal static string LoadPost(string userId)
    {
        string post;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserPost.txt"))
        {
            post = reader.ReadLine();
            reader.Close();
        }
        return post;
    }

    internal static void SaveNumber(string userId, string number)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserNumber.txt", false))
        {
            writer.Write(number);
            writer.Close();
        }
    }
    internal static string LoadNumber(string userId)
    {
        string number;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserNumber.txt"))
        {
            number = reader.ReadLine();
            reader.Close();
        }
        return number;
    }

    async internal static void DeleteUserData(string userId)
    {
        DirectoryInfo dir = new DirectoryInfo(mainDestination + "/" + userId);
        dir.Delete(true);
    }

    internal static string[] GetListOfWorkers()
    {        
        string[] folders = Directory.GetDirectories(mainDestination);
        return folders;
    }

    internal static void SaveMessage(string userId, int messageId)
    {
        using (StreamWriter writer = new StreamWriter(mainDestination + "/" + userId + "/UserMessages.txt", false))
        {
            writer.WriteLine(messageId);
            writer.Close();
        }
    }
    internal static int LoadMessage(string userId)
    {
        int messageId;
        using (StreamReader reader = new StreamReader(mainDestination + "/" + userId + "/UserMessages.txt"))
        {
            messageId = int.Parse(reader.ReadLine());
            reader.Close();
        }
        return messageId;
    }
}