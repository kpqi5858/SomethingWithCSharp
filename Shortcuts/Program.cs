using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Shortcuts
{
    class Program
    {
        const string DB_PATH = "C:\\kpqi\\shortcuts.txt";

        static void Main(string[] args)
        {
            if (!File.Exists(DB_PATH))
            {
                File.Create(DB_PATH);
                Console.WriteLine("Please do again!");
                Console.ReadKey();
                return;
            }
            bool reset = false;
            string argument = null;
            bool removeargument = false;
            if (args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    {
                        StreamReader sr1 = new StreamReader(DB_PATH);

                        List<string> readed1 = new List<string>();

                        string read1 = "";
                        while ((read1 = sr1.ReadLine()) != null)
                        {
                            readed1.Add(read1);
                        }

                        bool found1 = false;
                        string toExecute1 = null;
                        string line1 = null;
                        string augments1 = null;

                        foreach (string str in readed1)
                        {
                            string[] data = str.Split('?');
                            if (data.Length != 3)
                            {
                                Console.WriteLine("I readed wrong data!");
                                continue;
                            }
                            if (data[0].Equals(System.Reflection.Assembly.GetEntryAssembly().Location))
                            {
                                if (File.Exists(data[1]))
                                {
                                    found1 = true;
                                    toExecute1 = data[1];
                                    if (!data[2].Equals("nullnullnullnonononopenope"))
                                    {
                                        augments1 = data[2];
                                    }
                                    line1 = str;
                                }
                            }
                        }
                        sr1.Close();
                        if (found1) return;
                    }
                    
                    
                    

                    StreamWriter sw = new StreamWriter(DB_PATH, true);
                    sw.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location + "?" + Path.GetFullPath(args[0]) + "?" + "nullnullnullnonononopenope");
                    sw.Close();
                    return;
                }
                else if (args[0].Equals("reset"))
                {
                    reset = true;
                    Console.WriteLine("OK, Resetting...");
                }
                else if (args[0].Equals("removeargument"))
                {
                    removeargument = true;
                    Console.WriteLine("OK, Removing arguments...");
                }
                else
                {
                    Console.WriteLine("Please Drag and Drop file to me!");
                    Console.ReadKey();
                    return;
                }
            }
            if (args.Length >= 2)
            {
                if (args[0].Equals("argument"))
                {
                    StringBuilder sb = new StringBuilder();
                    bool first = true;
                    foreach(string str in args)
                    {
                        if (first)
                        {
                            first = false;
                            continue;
                        }
                        sb.Append(str);
                        sb.Append(" ");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    argument = sb.ToString();
                }
            }

            StreamReader sr = new StreamReader(DB_PATH);

            List<string> readed = new List<string>();

            string read = "";
            while ((read = sr.ReadLine()) != null)
            {
                readed.Add(read);
            }

            bool found = false;
            string toExecute = null;
            string line = null;
            string augments = null;

            foreach (string str in readed) {
                string[] data = str.Split('?');
                if (data.Length != 3)
                {
                    Console.WriteLine("I readed wrong data!");
                    continue;
                }
                if (data[0].Equals(System.Reflection.Assembly.GetEntryAssembly().Location))
                {
                    if (File.Exists(data[1]))
                    {
                        found = true;
                        toExecute = data[1];
                        if (!data[2].Equals("nullnullnullnonononopenope"))
                        {
                            augments = data[2];
                        }
                        line = str;
                    }
                }
            }
            sr.Close();

            if (!found && (reset || argument != null || removeargument))
            {
                Console.WriteLine("Myself have no shortcut!");
                Console.ReadKey();
                return;
            }

            if (found)
            {
                if (reset)
                {
                    readed.Remove(line);

                    StreamWriter sw = new StreamWriter(DB_PATH, false);
                    
                    foreach (string str in readed)
                    {
                        sw.WriteLine(str);
                    }
                    sw.Close();
                    Console.WriteLine("Removed the shortcut!");
                    Console.ReadKey();
                    return;
                }
                if (argument != null || removeargument)
                {
                    readed.Remove(line);

                    StringBuilder sb = new StringBuilder();

                    string[] splited = line.Split('?');

                    sb.Append(splited[0]);
                    sb.Append("?");
                    sb.Append(splited[1]);
                    sb.Append("?");
                    if (argument != null)
                    {
                        sb.Append(argument);
                    }
                    if (removeargument)
                    {
                        sb.Append("nullnullnullnonononopenope");
                    }

                    readed.Add(sb.ToString());

                    StreamWriter sw = new StreamWriter(DB_PATH, false);

                    foreach (string str in readed)
                    {
                        sw.WriteLine(str);
                    }
                    sw.Close();
                    Console.WriteLine("Arguments edited!");
                    Console.ReadKey();
                    return;
                }
                if (augments != null)
                {
                    ProcessStartInfo startinfo = new ProcessStartInfo();
                    startinfo.CreateNoWindow = false;
                    startinfo.UseShellExecute = false;
                    startinfo.FileName = toExecute;
                    startinfo.WindowStyle = ProcessWindowStyle.Normal;
                    startinfo.Arguments = augments;

                    Process.Start(startinfo);
                    return;
                }
                Process.Start(toExecute);
                return;
            } else
            {
                Console.WriteLine("Please Drag and Drop file to me!");
            }

            Console.ReadKey();
        }
    }
}
