using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public abstract class ValidationTemplate
    {
        public ValidationTemplate next;
        public void setNextChain(ValidationTemplate next)
        {
            this.next = next;
        }
        public abstract bool validate(Information info);
    }


    public class MonsterTypeValidator : ValidationTemplate
    {
        public override bool validate(Information info)
        {
            if (info.monstertype == 4)
            {
                return false;
            }
            else
            {
                if (next != null)
                {
                    return next.validate(info);
                }
                return true;
            }
            
        }
    }

    public class NameValidator : ValidationTemplate
    {
        public override bool validate(Information info)
        {
            if (info.name.Length < 1 || info.name.Length > 10)
            {
                return false;
            }
            else
            {
                if (next != null)
                {
                    return next.validate(info);
                }
                return true;
            }
        }
    }

    public class SwearwordValidator : ValidationTemplate
    {
        public override bool validate(Information info)
        {
            List<string> swearwords = new List<string>();

            swearwords.Add("bad");
            swearwords.Add("swear");
            swearwords.Add("horrible");
            swearwords.Add("gyvate");
            swearwords.Add("rupus miltai");


            foreach (string name in swearwords)
            {
                if (info.name.Contains(name))
                    return false;
            }

            if (next != null)
            {
                return next.validate(info);
            }
            return true;
        }
    }

    public class EmoteValidator : ValidationTemplate
    {
        public override bool validate(Information info)
        {
            if (info.emotetype == 3)
            {
                return false;
            }
            else
            {
                if (next != null)
                {
                    return next.validate(info);
                }
                return true;
            }

        }
    }

    public class Information
    {
        public Information(string Name, int Mtype, int Etype)
        {
            name = Name;
            monstertype = Mtype;
            emotetype = Etype;
        }
        public string name;
        public int monstertype;
        public int emotetype;
    }
}
