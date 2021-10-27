//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Runner2.Classes
//{
//    /// <summary>
//    /// "abstract factory" interface
//    /// </summary>
//    abstract class ItemFactory
//    {
//        public abstract Collectable CreateCollectable();
//        public abstract Potion CreatePotion();
        
//    }

//    /// <summary>
//    /// "concrete factory1" class
//    /// </summary>
//    class GoodItemFactory : ItemFactory
//    {
//        public override Collectable CreateCollectable()
//        {
//            return new Star();
//        }

//        public override Potion CreatePotion()
//        {
//            return new SpeedPotion();
//        }
//    }

//    /// <summary>
//    /// "concrete factory2" class
//    /// </summary>
//    class BadItemFactory : ItemFactory
//    {
//        public override Collectable CreateCollectable()
//        {
//            return new Bomb();
//        }

//        public override Potion CreatePotion()
//        {
//            return new SlowPotion();
//        }
//    }

//    /// <summary>
//    /// "abstract product A" interface
//    /// </summary>
//    abstract class Collectable
//    {
        
//    }
    
//    /// <summary>
//    /// "abstract product B" interface
//    /// </summary>
//    abstract class Potion
//    {
//    //    public abstract string name { get; set; }
//    //    public abstract float speedMod { get; set; }
//    //    public abstract float jumpMod { get; set; }
//    }

//    /// <summary>
//    /// "product a1" class
//    /// </summary>
//    class Star : Collectable
//    {

//    }
    
//    /// <summary>
//    /// "product a2" class
//    /// </summary>
//    class Bomb : Collectable
//    {
//    }

//    /// <summary>
//    /// "product b1" class
//    /// </summary>
//    class SpeedPotion : Potion
//    {
//        //public SpeedPotion()
//        //{
//        //    name = "speedPot";
//        //    speedMod = 2;
//        //    jumpMod = 1;
//        //}

//        //public override string name 
//        //{
//        //    get
//        //    {
//        //        return name;
//        //    } 
//        //    set
//        //    {
//        //        name = value;
//        //    }
//        //}
//        //public override float speedMod 
//        //{
//        //    get
//        //    {
//        //        return speedMod;
//        //    }
//        //    set
//        //    {
//        //        speedMod = value;
//        //    }
//        //}
//        //public override float jumpMod 
//        //{
//        //    get
//        //    {
//        //        return jumpMod;
//        //    }
//        //    set
//        //    {
//        //        jumpMod = value;
//        //    }
//        //}

//    }

//    /// <summary>
//    /// "product b2" class
//    /// </summary>
//    class SlowPotion : Potion
//    {
//        //public SlowPotion()
//        //{
//        //    name = "slowPot";
//        //    speedMod = -2;
//        //    jumpMod = 1;
//        //}

//        //public override string name
//        //{
//        //    get
//        //    {
//        //        return name;
//        //    }
//        //    set
//        //    {
//        //        name = value;
//        //    }
//        //}
//        //public override float speedMod
//        //{
//        //    get
//        //    {
//        //        return speedMod;
//        //    }
//        //    set
//        //    {
//        //        speedMod = value;
//        //    }
//        //}
//        //public override float jumpMod
//        //{
//        //    get
//        //    {
//        //        return jumpMod;
//        //    }
//        //    set
//        //    {
//        //        jumpMod = value;
//        //    }
//        //}
//    }
//}
