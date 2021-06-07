using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole
{
    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int IdNumber)
        {
            this.IdNumber = IdNumber;
        }
    }

    public class Personel
    {
        public int Age;
        public string Name;
        public IdInfo IdInfo;

        public Personel ShallowCopy()
        {
            return (Personel)this.MemberwiseClone();
        }

        public Personel DeepCopy()
        {
            Personel other = (Personel)this.MemberwiseClone();
            other.IdInfo = new IdInfo(IdInfo.IdNumber);
            other.Name = String.Copy(Name);
            return other;
        }
    }

    public class MyClone{
        public static void Test() {
            Personel person = new Personel();
            person.Age = 10;
            person.Name = "Sam";
            person.IdInfo = new IdInfo(100);
            Console.WriteLine("person.IdInfo NUmber" + person.IdInfo.IdNumber);
            var p = person.DeepCopy();
            p.IdInfo.IdNumber =11111111;
            Console.WriteLine("person.IdInfo NUmber" + person.IdInfo.IdNumber);
            Console.WriteLine("p.IdInfo NUmber" + p.IdInfo.IdNumber);
        }
        
        
    }

}
