using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class Game
    {
        private static Random Rand = new Random();

        private List<UIObject> DisplayedObjects = null;

        private Personage Personage = null;
        private List<Block> Blocks = null;

        private int TurnCounter = 0;
        private int BlockAppearRate = 30;
        private const int MaxBlockAppearRate = 10;
        private int DifficultyUpRate = 150;

        public int Score
        {
            private set;
            get;
        }

        public int Highscore
        {
            private set;
            get;
        }

        /// <summary>
        /// Constructor of the game
        /// </summary>
        public Game()
        {
            InitializeObjects(); // The initialize the objects

            // Initialize the scores
            Score = 0;
            Highscore = HighscoreFileHandler.getInstance().LoadHighscore();
            if (Highscore == -1)
                Highscore = 0;
        }

        /// <summary>
        /// Initializes the objects
        /// </summary>
        private void InitializeObjects()
        {
            DisplayedObjects = new List<UIObject>();

            // The ground
            DisplayedObjects.Add(new Ground(0, 550));

            // The player
            DisplayedObjects.Add(Personage = new Personage(400, 530));

            // The block list
            Blocks = new List<Block>();
        }

        /// <summary>
        /// Return the list of displayed objects
        /// </summary>
        /// <returns></returns>
        public List<UIObject> getDisplayedObjects()
        {
            List<UIObject> Objects = new List<UIObject>(DisplayedObjects);
            Objects.AddRange(Blocks);

            return Objects;
        }

        public void Turn(List<Action> a)
        {
            // Personage movement
            Personage.Move(a);
            // Blocks movement
            Blocks.ForEach(b => b.Move());

            // Make a block appear
            if (TurnCounter % BlockAppearRate == 0)
                BlockAppear();

            TurnCounter = (TurnCounter + 1) % DifficultyUpRate;

            if (TurnCounter == 0)
            {
                DifficultyUp();
                StrongBlockAppear();
            }

            // Destroy the blocks out of the window
            DestroyObsoleteBlocks();
        }
        
        /// <summary>
        /// Makes the game more difficult
        /// </summary>
        private void DifficultyUp()
        {
            if (BlockAppearRate > MaxBlockAppearRate)
                --BlockAppearRate;

            if (Block.BlockSpeed < Block.MaxBlockSpeed)
                ++Block.BlockSpeed;
        }

        /// <summary>
        /// Makes appear a block that will go on the position of the personage
        /// </summary>
        private void StrongBlockAppear()
        {
            int x;
            int y = ((Personage.Y * 2) + Personage.Height) / 2;
            int xSpeed = Block.BlockSpeed;
            int ySpeed = 0;

            int r = Rand.Next(2);
            // From the left
            if (r == 0)
            {
                x = 0;
            }
            // From the right
            else
            {
                x = 1050;
                xSpeed *= -1;
            }

            CreateBlock(x, y, xSpeed, ySpeed);
        }


        /// <summary>
        /// Make a random block appear
        /// </summary>
        private void BlockAppear()
        {
            int x, y, xSpeed, ySpeed;
            switch (Rand.Next(3))
            {
                // Block coming from the left
                case 0:
                    x = 0;
                    y = Rand.Next(530);
                    xSpeed = Block.BlockSpeed;
                    ySpeed = 0;
                    CreateBlock(x, y, xSpeed, ySpeed);
                    break;
                // Block coming from the top
                case 1:
                    x = Rand.Next(960);
                    y = 0;
                    xSpeed = 0;
                    ySpeed = Block.BlockSpeed;
                    CreateBlock(x, y, xSpeed, ySpeed);
                    break;
                // Block coming from the right
                case 2:
                    x = 1050;
                    y = Rand.Next(530);
                    xSpeed = -Block.BlockSpeed;
                    ySpeed = 0;
                    CreateBlock(x, y, xSpeed, ySpeed);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create a block
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="xSpeed">x speed</param>
        /// <param name="ySpeed">y speed</param>
        private void CreateBlock(int x, int y, int xSpeed, int ySpeed)
        {
            Blocks.Add(new Block(x, y, xSpeed, ySpeed));
        }

        /// <summary>
        /// Destroy blocks that are out of the window
        /// </summary>
        private void DestroyObsoleteBlocks()
        {
            // Make a copy of the list to not delete objects in the current list
            foreach (Block b in new List<Block>(Blocks))
                if (b.X < -30 || b.X > 1100 || b.Y < -30 || b.Y > 750)
                {
                    Blocks.Remove(b);
                    IncrementScore();
                }
        }

        /// <summary>
        /// Increment the score
        /// </summary>
        private void IncrementScore()
        {
            ++Score;
            if (Highscore < Score)
                Highscore = Score;
        }

        /// <summary>
        /// Verifies if a block overlaps the personage
        /// </summary>
        /// <returns>True if the game is over</returns>
        public Boolean VerifyEndOfGame()
        {
            foreach (Block b in Blocks)
                if (Overlaps(Personage, b))
                    return true;
            return false;
        }

        /// <summary>
        /// Checks if the personage and a block overlaps
        /// </summary>
        /// <param name="p">The personage</param>
        /// <param name="b">A block</param>
        /// <returns>True if they overlap</returns>
        private bool Overlaps(GameCore.Personage p, Block b)
        {
            List<Position> pCorners = new List<Position>();
            pCorners.Add(new Position(p.X, p.Y));
            pCorners.Add(new Position(p.X + p.Width, p.Y));
            pCorners.Add(new Position(p.X, p.Y + p.Height));
            pCorners.Add(new Position(p.X + p.Width, p.Y + p.Height));


            foreach (Position pos in pCorners)
                if (b.X < pos.X && pos.X < b.X + b.Width
                    &&
                    b.Y < pos.Y && pos.Y < b.Y + b.Height)
                    return true;

            return false;
        }
    }
}
