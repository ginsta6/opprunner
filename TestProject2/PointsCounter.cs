using System;
using Xunit;
using Runner2.Classes;

namespace TestProject2
{
    public class PointsCounter
    {
        [Fact]
        public void TestResetPoints()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.ResetPoints();
            int points = pl.Points.points;
            Assert.Equal(0, points);
        }
        [Fact]
        public void TestAddPoints()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.points = 0;
            pl.Points.AddPoints(10);
            int points = pl.Points.points;
            Assert.Equal(10, points);
        }
        [Fact]
        public void TestAddNegativePoints()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.points = 0;
            pl.Points.AddPoints(-10);
            int points = pl.Points.points;
            Assert.Equal(-10, points);
        }
        [Fact]
        public void TestSubtractPoints()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.points = 20;
            pl.Points.SubtractPoints(10);
            int points = pl.Points.points;
            Assert.Equal(10, points);
        }  
        [Fact]
        public void TestSubtractNegativePoints()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.points = 20;
            pl.Points.SubtractPoints(-10);
            int points = pl.Points.points;
            Assert.Equal(30, points);
        }
        [Fact]
        public void TestPointsShallowCopy()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.points = 15;
            var clone = ((Runner2.Classes.PointsCounter)pl.Points.shallowCopy()).points;

            Assert.Equal(15, clone);
        }
        [Fact]
        public void TestPointsDeepCopy()
        {
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");

            pl.Points.points = 35;
            var clone = ((Runner2.Classes.PointsCounter)pl.Points.deepCopy()).points;

            Assert.Equal(35, clone);
        }

        [Fact]
        public void TestObstacleRemovePoints()
        {
            AbstractSceneFactory sceneF = new WinterFactory();
            Obstacle obs = sceneF.CreateObstacle();
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");
            int points = 35;
            pl.Points.points = points;
            //Item ite; 
            // ObstacleAdapter explodedItem;// = new ObstacleAdapter(obs);
            obs.removePoints(pl);
            points -= obs.pointsModifier;

            Assert.Equal(points, pl.Points.points);
        }
        [Fact]
        public void TestObstacleRemovePoints1()
        {
            AbstractSceneFactory sceneF = new WinterFactory();
            Obstacle obs = sceneF.CreateObstacle();
            Creator playerF = new ConcreteCreator();
            Player pl = playerF.FactoryMethod("Owlet");
            int points = 35;
            pl.Points.points = points;
            //Item ite; 
            // ObstacleAdapter explodedItem;// = new ObstacleAdapter(obs);
            obs.removePoints(pl);
            points -= obs.pointsModifier;

            Assert.Equal(points, pl.Points.points);
        }

    }
}
