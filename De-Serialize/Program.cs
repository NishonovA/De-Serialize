using System;
using System.IO;
using SerialLib;

namespace De_Serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Serialized.txt";
            using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                ListRand list = new ListRand();
                ListGenerator(list);
                list.Show();
                list.Serialize(fstream);
            }
            using (FileStream fstream = File.OpenRead(fileName))
            {
                ListRand new_list = new ListRand();
                new_list.Deserialize(fstream);
                new_list.Show();
            }
            Console.ReadKey();
        }

        static void ListGenerator(ListRand list)
        {
            list.AddNode(new ListNode { Data = "Alex" });
            list.AddNode(new ListNode { Data = "Bob" });
            list.AddNode(new ListNode { Data = "Chr;\r\nis" });
            list.AddNode(new ListNode { Data = "Daniel" });
            list.AddNode(new ListNode { Data = "Em   il" });
            list.AddNode(new ListNode { Data = "Filthy" });
            list.AddNode(new ListNode { Data = "Fra nk" });
            list.AddNode(new ListNode { Data = "Geo;\\r\\nrge" });
            list.AddNode(new ListNode { Data = "Har  old" });
            list.AddNode(new ListNode { Data = "Irwin" });
            list.DelNode(6);

            Random rnd = new Random();
            ListNode Cur = list.Head;
            for (int i = 1; i <= list.Count; i++)
            {
                Cur.Rand = list.IndexToNode(rnd.Next(list.Count) + 1);
                Cur = Cur.Next;
            }
        }
    }
}
