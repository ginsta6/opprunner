using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public abstract class Symbol
    {
        public abstract string getPictureString();
    }

    public class QuestionMark : Symbol
    {
        public override string getPictureString()
        {
            return "pack://application:,,,/Images/question.png";
        }
    }

    public class ExclamationPoint : Symbol
    {
        public override string getPictureString()
        {
            return "pack://application:,,,/Images/exclamation.png";
        }
    }
}
