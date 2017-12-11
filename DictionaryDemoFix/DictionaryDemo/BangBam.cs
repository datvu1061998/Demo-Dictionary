using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryDemo
{
    public class BangBam
    {
        public DSLK[] Table;

        public static int DemDong()
        {
            FileStream file = new FileStream(
               @"C:\Users\Trung Kien\Documents\GitHub\Do-An\DictionaryDemoFix\DictionaryDemo\input.txt",
               FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(file);
            string s = sr.ReadLine();
            int d = 0;
            do
            {
                d++;
                s = sr.ReadLine();
            }
            while (s != null);
            sr.Close();
            file.Close();
            return d;
        }
        int dong = DemDong();

        public BangBam()
        {
            Table = new DSLK[dong];
            for (int i = 0; i < dong; i++)
            {
                Table[i] = new DSLK();
            }
        }

        public int Bam(string s)
        {
            int x = 1;
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                if (s[i] < 'a' || s[i] > 'z')
                    i++;
                else
                    x = x * (s[i] - 96) % dong;
            }
            return x;
        }

        public void Add(Word wd)
        {
            int bam = Bam(wd.Name);
            bool check = Search_Check(wd.Name);
            try
            {
                if (check == true)
                    throw new Exception("This word has been added");
                Node p = new Node();
                p.CreateNode(wd);
                Table[bam].AddTail(p);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool Search_Check(string s)
        {
            int bam = Bam(s);
            Node p = new Node();
            p = Table[bam].Head;
            while (p != null)
            {
                if (p.word.Name == s)
                    return true;
                p = p.pNext;
            }
            return false;
        }

        public Node Search_Lookup(string s)
        {
            int bam = Bam(s);
            bool check = Search_Check(s);
            try
            {
                if (check == false)
                {
                    throw new Exception("HashTable doesn't have this word");
                }
                // sua
                Node p = new Node();
                p = Table[bam].Search(s);
                return p;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void RemoveWord(string s)
        {
            int bam = Bam(s);
            if (s == Table[bam].Head.word.Name)
                Table[bam].RemoveHead();
            else
            {
                Node p = new Node();
                p = Table[bam].SearchTruoc(s);
                try
                {
                    if (Table[bam].Head == null || p == null || Table[bam].Head == Table[bam].Tail || p.pNext == null)
                        throw new Exception();
                    Table[bam].RemoveAfter(p);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void EditWord(string name, string meaning, string explication)
        {
            int bam = Bam(name);
            bool check = Search_Check(name);
            try
            {
                if (check == false)
                {
                    throw new Exception("This word is not had in HashTable!!!");
                }
                Node p = new Node();
                p = Table[bam].Head;
                while (p != null)
                {
                    if (p.word.Name == name)
                    {
                        p.word.Meaning = meaning;
                        p.word.Explication = explication;
                    }
                    p = p.pNext;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
