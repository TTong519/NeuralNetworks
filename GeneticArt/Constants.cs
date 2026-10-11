using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticArt
{
    public static class Constants
    {
        public const int CANVAS_WIDTH = 50;
        public const int CANVAS_HEIGHT = 50;
        public const double COLOR_MUTATION_CHANCE = 0.5;
        public const int COLOR_MUTATION_AMOUNT_MAX = 64;
        public const int COLOR_MUTATION_AMOUNT_MIN = -64;
        public const double POINT_MUTATION_AMOUNT_MAX = 0.25;
        public const double POINT_MUTATION_AMOUNT_MIN = -0.25;
        public const byte RAND_COLOR_A_MIN = 31;
        public const byte RAND_COLOR_A_MAX = 224;
        public const double RAND_TRIANGLE_POINT_MIN = 0.0;
        public const double RAND_TRIANGLE_POINT_MAX = 0.5;
        public const double ADD_TRIANGLE_CHANCE = 0.4;
        public const double REMOVE_TRIANGLE_CHANCE = 0.2;
        public const double MUTATE_TRIANGLE_CHANCE = 0.4;
        public const int POPULATION_SIZE = 100;
        public const int TRIANGLE_MAX = 100;
    }
}
