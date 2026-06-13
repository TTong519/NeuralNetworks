using CommonLib;
using static CommonLib.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;

namespace FlappyBirdGeneticLearning
{
    public static class BirdLearner
    {
        private static GeneticLearner learner;
        private static Dictionary<NeuralNetwork, double> fitnessDict;
        private static List<Bird> birds;
        private static int deathCount = 0;
        public static void Init()
        {
            learner = new GeneticLearner(100, 2, (NeuralNetwork nn) => fitnessDict[nn], ErrorFuncs.MSE, ActivationFuncs.Identity, [2, 4, 1]);
            fitnessDict = new Dictionary<NeuralNetwork, double>();
            birds = new List<Bird>();
            for(int i = 0; i < 100; i++)
            {
                birds.Add(new Bird(new Rectangle(0, 100, 60, 50), 0.5));
                birds[i].NeuralNetwork = learner.population[i];
            }
        }
        public static bool Update(List<Pipe> pipes, GraphicsDevice graphicsDevice)
        {
            if(deathCount == 100)
            {
                foreach(Bird bird in birds)
                {
                    fitnessDict[bird.NeuralNetwork] = bird.Score;
                }
                learner.Learn(1, 0.1, 0.5);
                birds.Clear();
                fitnessDict.Clear();
                for (int i = 0; i < 100; i++)
                {
                    birds.Add(new Bird(new Rectangle(0, 300, 60, 50), 0.5));
                    birds[i].NeuralNetwork = learner.population[i];
                }
                deathCount = 0;
                return true;
            }
            foreach(Bird b in birds)
            {
                if(b.Update(pipes, graphicsDevice, b.NeuralNetwork.Compute(new double[] { System.Math.Abs((pipes.First().Location.X + 50) - b.Bounds.Center.X), b.Bounds.Center.Y - pipes.First().Location.Y })[0] >= 0.0))
                {
                    deathCount++;
                }
            }
            return false;
        }
        public static void Draw(SpriteBatch spriteBatch, Texture2D birdTexture)
        {
            foreach (Bird b in birds)
            {
                b.Draw(spriteBatch, birdTexture);
            }
        }
    }
}
