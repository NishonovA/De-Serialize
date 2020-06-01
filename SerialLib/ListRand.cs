using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SerialLib
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        public void Serialize(FileStream s) //Сериализация
        {
            ListNode Cur = Head;
            int index = 0;
            string id;
            string id_rand;
            string name;
            while (Cur != null)    //
            {
                index++;
                id = Convert.ToString(index);
                id_rand = Convert.ToString(NodeToIndex(Cur.Rand));
                name = Cur.Data;
                name = name.Replace("\n", "\n\n");
                name = name.Replace("\r", "\r\r");
                s.Write(Encoding.Default.GetBytes($"{id} {id_rand} {name};" + Environment.NewLine));
                Cur = Cur.Next;
            }
        }

        public void Deserialize(FileStream s)   //Десериализация
        {
            byte[] array = new byte[s.Length];
            s.Read(array, 0, array.Length);
            string text = Encoding.Default.GetString(array);
            Dictionary<int, string> names = new Dictionary<int, string>();
            Dictionary<int, int> rands = new Dictionary<int, int>();

            string[] lines = text.Split(";\r\n");
            for (int k=0; k < lines.Length - 1; k++)
            {
                string[] blocks = lines[k].Split(' ');
                rands.Add(Convert.ToInt32(blocks[0]), Convert.ToInt32(blocks[1]));
                string name = blocks[2];
                for (int i = 3; i < blocks.Length; i++)
                {
                    name = name + " " + blocks[i];
                }
                name = name.Replace("\n\n", "\n");
                name = name.Replace("\r\r", "\r");
                names.Add(Convert.ToInt32(blocks[0]), name);
            }

            for (int i = 1; i <= names.Count; i++)
            {
                ListNode temp = new ListNode { Data = names[i] };
                this.AddNode(temp);
            }
            for (int i = 1; i <= names.Count; i++)
            {
                this.IndexToNode(i).Rand = this.IndexToNode(rands[i]);
            }
        }

        public void AddNode (ListNode elem) //Добавление элемента списка
        {
            if (Head == null)
            {
                Head = elem;
                Tail = elem;
                Count = 1;
            }
            else
            {
                Count++;
                Tail.Next = elem;
                elem.Prev = Tail;
                Tail = elem;
            }
        }

        public void DelNode (int number)    //Удаление элемента списка
        {

            if (Count !=0)
            {
                ListNode Cur = IndexToNode(number);
                if (ReferenceEquals(Cur, Head))
                {

                    if (Count > 1)
                    {
                        Head = Cur.Next;
                        Cur.Next.Prev = null;
                    }
                    else
                    {
                        Head = null;
                    }
                }
                else if (ReferenceEquals(Cur, Tail))
                {
                    Tail = Cur.Prev;
                    Cur.Prev.Next = null;
                }
                else
                {
                    Cur.Prev.Next = Cur.Next;
                    Cur.Next.Prev = Cur.Prev;
                }
                Count--;
            }
            else
            {
                Console.WriteLine("Удаление невозможно. Списка нет!");
            }
        }

        public int NodeToIndex (ListNode elem)  //Номер элемента в списке
        {
            int index = 1;
            ListNode Cur = Head;
            while (!ReferenceEquals(elem, Cur))
            {
                Cur = Cur.Next;
                index++;
            }
            return index;
        }

        public ListNode IndexToNode (int index) //Элемент по номеру
        {
            ListNode Cur = Head;
            int counter = 1;
            while (counter!=index)
            {
                Cur = Cur.Next;
                counter++;
            }
            return Cur;
        }

        public void Show () //Вывод на экран
        {
            ListNode Cur = Head;
            if (Cur != null)
            {
                Console.WriteLine("Текущий список:");
                while (Cur != null)
                {
                    Console.WriteLine("- " + Cur.Data + " -> " + Cur.Rand.Data);
                    Cur = Cur.Next;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Списка нет.");
                Console.WriteLine();
            }
        }
    }
}
