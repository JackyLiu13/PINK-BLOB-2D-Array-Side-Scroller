using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssignmentTwoSideScroller
{
    /* Assignment 2 - Side scroller, kirby run
     * Jacky Liu
     * December 2nd 2019
     * Gr 11 Mr.Hsiung Period 4
     * Side Scrolling game with the use for for loops and arrays
     * through projectiles,enemies,backgrounds,players ,etc
    */
    public partial class SideScroller : Form
    {
        //VARIABLES BELLOW ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // ~~~~~ Variable for player ~~~~~~ obvious names 
        #region Player Variables
            //these are dimensions for drawing the player 
        const int PLAYER_WIDTH = 50, PLAYER_HEIGHT = 50;
        //used for setting the player's location
        int playerX = 0, playerY = FLOOR;
        //used for the speed for the player to either mmove up left down right
        int playerVelocityX = 0, playerVelocityY = 0;
        //constant speeds that the player will be going
        const int RUN_SPEED = 10, JUMP_SPEED = -20, GRAVITY = 2;
        //used for the location and size of the player
        Rectangle playerBox;
        //images for the player, this is a array of 4 because it stores all the directions of the player that is going
        //giving a slightly friendlier UI
        Image[] playerImage = new Image[4];
        #endregion
        // ~~~~~~~ variable for window/background screen ~~~~~~~~~
        #region Window Variables
        // constant sizing of the window
        const int WINDOW_W = 1200, WINDOW_H = 534;
        //speed of the background when scrolling on the first background
        const int BACKGROUND_SPEED = 10;
        //the sizing location of the background
        Rectangle backgroundBox;
        //the images for the background
        Image[] backgroundImage = new Image[10];
        //location of the floor to sync with the proper drawing of the background
        const int FLOOR = 350;
        //used for drawing the proper screens
        bool sideScrollScreen = true, dungeonScreen = false;
        //used for the drawing of the background 
        int backgroundIndex = 4;
        //number of dungeon in the game
        const int DUNGEON_AMNT = 3;
        //checks if the end screen or start screen
        bool gameIsRunning = false, gameEnd = false;
        #endregion

        // ~~~~~~~ Variable for the hole to enter the dungeon iiin the side scrller ~~~~~~~ 
        #region Hole
        //c onstant location and size of it
        const int HOLE_X = 738, HOLE_Y = FLOOR - HOLE_H, HOLE_W = 38, HOLE_H = 51;
        //used to store where to draw
        Rectangle holeBox;
        //used to store the image
        Image holeImage;
        #endregion
        // ~~~~~~~~ Variable for enemies ~~~~~~~~~~~~~
        #region Enemy Variables
            //used for storing how many enemies per dungeon
        int[] numOfEnemy = new int [3];
        //2d array, first number is number of dungeon, 5 is the maximum number of enemies per dungeon
        int[,] enemyX = new int[3, 5], enemyY = new int[3, 5];
        //used for the constant size of enemy
        const int ENEMY_WIDTH = 50, ENEMY_HEIGHT = 50;
        //used for the maximum amount of enemies
        int maxEnemyNum = 5;
        //used for store the location of each enemy
        Rectangle[,] enemyBox = new Rectangle[4, 5];
        //used for storing the image of thenemy
        Image enemyImage;
        #endregion
        // ~~~~~~~~ Projectile 
        #region Projectile Variables
        //2d array, first number is number of dungeon, 5 is the maximum number of enemies per dungeon
        //uused for storing ocation of projectile
        Rectangle[,] projectileBox = new Rectangle[3,5];
        //animatim image of projectile
        Image[] projectileImage = new Image[8];
        //location of each projectile non const since they moving
        int[,] projectileX = new int[3,5], projectileY = new int[3,5];
        //checks whether they movin
        bool[,] projectileMovement = new bool [3,5];
        //used for setting their destination
        int[,] rise = new int[3,5], run = new int[3,5];
        //the location it gonna go
        double[,] hypotenuse = new double [3,5];
        //used for find the player at the appropraite velocity
        int[,] projectileXSpeed = new int[3,5], projectileYSpeed = new int[3,5];
        //cinst speed of the projectile
        const int SPEED_OF_PROJECTILE = 5;

        #endregion
        //used for animating prjecilte
        int[] projectileAnimationIndex;
        //used for giving frinedlyish ui for the player in proper direction its facing
        int playerDirectionImageIndex;
        //const num for the proper lcoation
        const int RIGHT = 0, LEFT = 1, UP = 2, DOWN = 3;
        //used for storing the users name for the end
        string userName;

        // ~~~~ random generator ~~~~~
        Random numberGenerator = new Random();

        //VARIABLES ABOVE  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        //timer for tihngs related to theplayers movement
        private void tmrMovements_Tick(object sender, EventArgs e)
        {
            //moving the player up down left right, and the gravity
            PlayerMovements();
            //used for transitiong the screens
            Transition();
            //ReLocatePlayers the playr
            ReLocatePlayer();
            //refrehses everything
            Refresh();

        }

        //resets the projectiles to somewhere far so it doesnt draw at 0,0 top left corner
        //when dungeon starts
        void ResetProjectiles()
        {
            //for every dungeon...
            for (int i = 0; i < projectileBox.GetLength(0); i++)
            {
                //for every enemy within the dugneon it will send them somewhere far
                for (int j = 0; j < numOfEnemy[backgroundIndex - 1]; j++)
                {
                    projectileX[i, j] = 11000;
                    projectileY[i, j] = 11000;
                }
            }
        }
        //when form loads
        public SideScroller()
        {
            //built in 
            InitializeComponent();
            //sets up the background's initial variables
            InitializeBackground();
            //sets up the image's inital variables
            InitializeProjectileImage();
            //sets up the projectile somewhee far 
            //ResetProjectiles();
            //sets up the player's initial variables
            InitializePlayer();

        }
        #region Projectiles
        //resizes evyerthing so it fits the appropriate number of projectile per enemy in dungeon
        void ResizeProjectileArray()
        {
            //all does the same, for 3 dungeons, it will set the proper 
            projectileMovement = new bool[3, numOfEnemy[backgroundIndex - 1]];
            //sets the starting position of the projectile
            projectileX = new int[3, numOfEnemy[backgroundIndex - 1]];
            projectileY = new int[3, numOfEnemy[backgroundIndex - 1]];
            rise = new int[3, numOfEnemy[backgroundIndex - 1]];
            run = new int[3, numOfEnemy[backgroundIndex - 1]];
            hypotenuse = new double[3, numOfEnemy[backgroundIndex - 1]];
            projectileXSpeed = new int[3, numOfEnemy[backgroundIndex - 1]];
            projectileYSpeed = new int[3, numOfEnemy[backgroundIndex - 1]];
            projectileBox = new Rectangle[3, numOfEnemy[backgroundIndex - 1]];
            //sends them far
            ResetProjectiles();
            
            
        }
        //assigns the imaging variables
        void InitializeProjectileImage()
        {
            //sets the proper images
            projectileImage[0] = Properties.Resources.projectile_0;
            projectileImage[1] = Properties.Resources.projectile_1;
            projectileImage[2] = Properties.Resources.projectile_2;
            projectileImage[3] = Properties.Resources.projectile_3;
            projectileImage[4] = Properties.Resources.projectile_4;
            projectileImage[5] = Properties.Resources.projectile_5;
            projectileImage[6] = Properties.Resources.projectile_6;
            projectileImage[7] = Properties.Resources.projectile_7;
            //resizes the animation index to the max
            projectileAnimationIndex = new int[projectileImage.Length];
        }
        //used for animating the projectile
        void AnimateProjectile()
        {
            //for every animation per projectile
            for (int i = 0; i < projectileAnimationIndex.Length; i++)
            {
                // will add a 1 of the index counter
                projectileAnimationIndex[i]++;
                //checks whether if the projecile index reaches the length of the images
                if (projectileAnimationIndex[i] == projectileImage.Length)
                {
                    //resets it
                    projectileAnimationIndex[i] = 0;
                }
            }
        }

        //use for spawning the projectile every 2 seconds
        private void tmrProjectileSpawn_Tick(object sender, EventArgs e)
        {
            //checks whter if player is in the dungeon, will generate projectile
            if (dungeonScreen == true)
            {
                ProjectileGeneration();
            }
        }


        //used for generating the projeciles
        void ProjectileGeneration()
        {
            //For every enemy in the dungeon will
            //check whether if not they are moving
            // if they aren't moving then it will reset their position
            // find their new destination using slope and their speed
            // put them back into movement (set the statement to true)

            for (int i = 0; i < numOfEnemy[backgroundIndex - 1]; i++)
            {
                for (int j = 0; j < projectileMovement.GetLength(0); j++)
                {
                    
                    if (projectileMovement[j, i] == false)
                    {
                        //sets the starting position of the projectile
                        projectileX[j, i] = enemyBox[backgroundIndex - 1, i].X;
                        projectileY[j, i] = enemyBox[backgroundIndex - 1, i].Y;

                        //calculate the slope from the enemy to the player
                        rise[j, i] = playerY - enemyBox[backgroundIndex - 1, i].Y;
                        run[j, i] = playerX - enemyBox[backgroundIndex - 1, i].X;
                        hypotenuse[j, i] = (double)Math.Sqrt(rise[j, i] * rise[j, i] + run[j, i] * run[j, i]);
                        //calulate the speeds for the projectile using slope 
                        //projectile x speed is run divided by hypotenuse and is multiplyed by the projectile's speed
                        //so it can move horiztonally
                        projectileXSpeed[j, i] = Convert.ToInt32(run[j, i] / hypotenuse[j, i] * SPEED_OF_PROJECTILE);
                        //projectile y speed is rise divided by hypotenuse and is multiplyed by the projectile's speed
                        //so it can move vertically
                        projectileYSpeed[j, i] = Convert.ToInt32(rise[j, i] / hypotenuse[j, i] * SPEED_OF_PROJECTILE);
                        projectileMovement[j, i] = true;
                        break;
                    }
                }
            }

        }
        //used for moving the projectiles
        void ProjectileMovement()
        {
            //checks if in the dungeon
            if (dungeonScreen == true)
            {
                //for every dungeon...
                for (int k = 0; k < projectileMovement.GetLength(0); k++)
                {
                    // for every eneemy.. will keep the porjectile moving horizontally and vertically
                    //sets the proper location, and checks whether if it touches any borders
                    //will regenerate projectile, also send it far away so it does draw on screen until it is assigned
                    //and if touches player will end the game as a lost
                    for (int i = 0; i < projectileMovement.GetLength(1); i++)
                    {
                        //projectilebox.X is added to projectile x speed
                        projectileX[k, i] += projectileXSpeed[k, i];
                        //projectilebox.y is added to projectile y speed
                        projectileY[k, i] += +projectileYSpeed[k, i];
                        //sets the proper location,
                        projectileBox[k, i] = new Rectangle(projectileX[k, i], projectileY[k, i], (PLAYER_WIDTH), (PLAYER_HEIGHT));
                        //if it touches any borders
                        //will regenerate projectile, also send it far away so it does draw on screen until it is assigned
                        if (projectileBox[k, i].Y < 0 || projectileBox[k, i].Y > FLOOR - 50 ||
                            projectileBox[k, i].X < 0 || projectileBox[k, i].X > ClientSize.Width)
                        {
                            //projectile in motion is set to false, and then is turned back on in the subprogram above
                            
                            projectileMovement[k, i] = false;
                            //sends far away
                            projectileX[k, i] = 10000;
                            projectileY[k, i] = 10000;
                        }

                        // if touches player will end the game as a lost
                        else if (projectileBox[k, i].IntersectsWith(playerBox))
                        {
                            //shows lost screen
                            backgroundIndex = 8;
                            lblEnd.Text = "Oh no " + userName + " you lost D:\nbetterluck next time :)";
                            lblEnd.Show();
                            //stops the game and prepares for the end
                            EndGame();
                        }
                    }
                }
            }
        }
        #endregion 
        //used for drawing the projectile
        private void tmrProjectile_Tick(object sender, EventArgs e)
        {
            //aniamtes projecile
            AnimateProjectile();
            //moves it
            ProjectileMovement();

        }
        
        //spawns the enemy
        void InitializeEnemies()
        {
            //collects how many max enemy
            // for every dungeon (3) it will generate a new amount of enemy
            // for every random number of enemy in the dungeon
            // it will set their location at a random point
            enemyX = new int[DUNGEON_AMNT, maxEnemyNum];
            enemyY = new int[DUNGEON_AMNT, maxEnemyNum];
            enemyBox = new Rectangle[DUNGEON_AMNT, maxEnemyNum];
            for (int dungeonIndex = 0; dungeonIndex < DUNGEON_AMNT; dungeonIndex++)
            {
                //sets how many enemies are in this current dungeon
                numOfEnemy[dungeonIndex] = numberGenerator.Next(3, maxEnemyNum + 1);
                //for every enemy within the dungeon
                //generates a random location for 
                for (int j = 0; j < numOfEnemy[dungeonIndex]; j++)
                {
                    enemyX[dungeonIndex, j] = numberGenerator.Next(ClientSize.Width / 2, ClientSize.Width - ENEMY_HEIGHT);
                    enemyY[dungeonIndex, j] = numberGenerator.Next(0, FLOOR - ENEMY_HEIGHT*3);
                    enemyBox[dungeonIndex, j] = new Rectangle(enemyX[dungeonIndex, j], enemyY[dungeonIndex, j], (ENEMY_WIDTH), (ENEMY_HEIGHT));
                }
            }
            enemyImage = Properties.Resources.enemy;


        }
        #region

        void PlayerMovements()
        {
            //keeps player moving horizontaly and vertically
            playerY += playerVelocityY;
            //adds some gravity as the player acends, which makes it decend
            playerVelocityY += GRAVITY;
            playerX += playerVelocityX;
            //left side border for the side scrooling
            if (playerX <= 0 && (backgroundIndex == 0))
            {
                playerX = 0;
            }
            //checks if the player is touching tbhe top, if true keeps it from going up in c=e player some how reaches top of screen
            if (playerY <= 0)
            {
                playerY = 0;
            } 
            //checks if player is over or touching the floor side of the screen, if true keeps from going out 
            else if (playerY >= FLOOR)
            {
                playerY = FLOOR;
            }
            //prevents player from falling through the floor by removing any velcoity
            if (playerY + PLAYER_HEIGHT >= FLOOR)
            {
                playerY = FLOOR - PLAYER_HEIGHT;
                playerVelocityY = 0;
            }
            //used for setting the proper iamges for player
            //checks if its speed is greater 0 (max height) will show player falling
            if (playerVelocityY > 0)
            {
                playerDirectionImageIndex = DOWN;
            }
            //used for checking if the player's speed between 0 and jumping speed (show its going up) will draw player jumping
            else if (playerVelocityY >= JUMP_SPEED && playerVelocityY < 0)
            {
                playerDirectionImageIndex = UP;
            } 
            //if player horizontal speed is positive means goign right, draws right
            else if (playerVelocityX == RUN_SPEED)
            {
                playerDirectionImageIndex = RIGHT;

            }
            //if player horizontal speed is negative means goign left, draws left
            else if (playerVelocityX == -RUN_SPEED)
            {
                playerDirectionImageIndex = LEFT;

            }
            //ReLocatePlayers so player is at proper location
            ReLocatePlayer();

        }
        //"relocates"/put in proper location the player with the variables
        void ReLocatePlayer()
        {
            playerBox = new Rectangle(playerX, playerY, (PLAYER_WIDTH), (PLAYER_HEIGHT));

        }
        //used transitioning the backgrounds
        void Transition()
        {
            //used for the side scrolling on the first page~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`
            if (sideScrollScreen == true)
            {
                //checks if the player is at or beyond the third of the screen
                //and if the background's location is great or equal to the size of the client
                //and that the player is going right (usisng positive velocity)
                // then the background will scroll to the left, and the player will also move to the left
                // but since the player is going right it will cancel out leaving the player in place
                //giving the "illusion" that the player is moving
                if (playerX >= ClientSize.Width / 3 && backgroundBox.X >= -ClientSize.Width && playerVelocityX == RUN_SPEED)
                {
                    backgroundBox.X -= BACKGROUND_SPEED;
                    playerX -= BACKGROUND_SPEED;
                }
                //checks if the player is at or less the third of the screen
                //and if the background's location is less than 0 (the starting position)
                //and that the player is going left (usisng neg. velocity)
                //same idea as above but goes inverted
                else if (playerX <= ClientSize.Width / 3 && playerVelocityX == -RUN_SPEED && backgroundBox.X < 0)
                {
                    backgroundBox.X += BACKGROUND_SPEED;
                    playerX += BACKGROUND_SPEED;
                }
                //draws the hole to go into the dungeon
                holeBox = new Rectangle(backgroundBox.X + ClientSize.Width * 2 - HOLE_W * 3, HOLE_Y, HOLE_W, HOLE_H);

                //prevents player from jumping over the hole 
                if (playerX >= holeBox.X - HOLE_W)
                {
                    playerX = holeBox.X - HOLE_W;
                }
                //first time check if the player goes into the hole 
                //then it will go into the dungeon making the first screen false and second true
                //index changes so that it will draw the next bkacground
                //resets the location of the background
                //spawns the enemies)
                if (playerX >= backgroundBox.X + ClientSize.Width * 2 - HOLE_W * 3 - PLAYER_WIDTH && playerY >= HOLE_Y)
                {
                    //draws the hole to go into the dungeon
                    //dispears into infiniftyh and beyond
                    holeBox = new Rectangle(ClientSize.Width * 5, ClientSize.Width * 5, HOLE_W, HOLE_H);
                    //sets proper backgrounds
                    sideScrollScreen = false;
                    dungeonScreen = true;
                    backgroundBox = new Rectangle(0, 0, (ClientSize.Width), (ClientSize.Height));
                    backgroundIndex++;
                    //resetns player to left size
                    playerX = 0;
                    //gives proper projectile array sizing and location
                    ResizeProjectileArray();
                    ProjectileGeneration();
                    //ses up the projectile timer
                    tmrProjectileSpawn.Enabled = true;
                    tmrProjectile.Enabled = true;
                }
            }
           //During the first index, the character cannot go left
            if (dungeonScreen == true && playerX <= 0 && backgroundIndex == 1)
            {
                playerX = 0;
                ReLocatePlayer();
            }
            //during the second index, the character can go left, but will return to the right side for the first index
            //while the player is still in the dungeon and is on the right side and the has not reached the last dungeon
            else if (dungeonScreen == true && playerX >= (ClientSize.Width - PLAYER_WIDTH) && backgroundIndex < DUNGEON_AMNT )
            {
                //draws the other screen
                backgroundIndex++;
                //resizes and relocate projectiles
                ResizeProjectileArray();
                ProjectileGeneration();
                //sets player on the left side of the screen (10 because it will prevent screen from spasing out)
                playerX = 10;
            }
            //checks if player is going left and is not on the first dungeon
            else if  (dungeonScreen == true && playerX <= 0 && backgroundIndex > 1)
            {
                //draws one back
                backgroundIndex--;
                //resizes and relocate projectiles
                ResizeProjectileArray();
                ProjectileGeneration();
                //sets player to be left side of screen, -1 because it no interfere with border
                playerX = ClientSize.Width - PLAYER_WIDTH - 1;
            }
            //if he player reaches the end of the last dungeon they win
            else if (dungeonScreen == true && playerX >= ClientSize.Width - PLAYER_WIDTH && backgroundIndex == DUNGEON_AMNT)
            {
                //gets the end game setup
                EndGame();
                backgroundIndex = 9;
                lblEnd.Text = "Good Job " + userName + " you won :D";
                lblEnd.Show();
            }
               

        }
        //ses up the end game objects and removes the game timer and objects
        void EndGame()
        {
            gameIsRunning = false;
            tmrMovements.Enabled = false;
            tmrProjectileSpawn.Enabled = false;
            tmrProjectile.Enabled = false;
            gameEnd = true;
            //redraws the change
            Refresh();

        }
        //sets up the inital background variable and the hole
        void InitializeBackground()
        {
            //Sets the background to the size of the window and sets it to 0,0
            // allowing it draw the big background
            backgroundBox = new Rectangle(0, 0, (ClientSize.Width ), (ClientSize.Height));
            backgroundImage[0] = Properties.Resources.background;
            backgroundImage[1] = Properties.Resources.dungeon_1;
            backgroundImage[2] = Properties.Resources.dungeon_2;
            backgroundImage[3] = Properties.Resources.dungeon_3;
            backgroundImage[4] = Properties.Resources.start_screen_1;
            backgroundImage[5] = Properties.Resources.start_screen_4;
            backgroundImage[6] = Properties.Resources.start_screen_2;
            backgroundImage[7] = Properties.Resources.start_screen_3;
            backgroundImage[8] = Properties.Resources.you_lost;
            backgroundImage[9] = Properties.Resources.you_win;
            //sets up the hole of where it is and its dimension
            holeBox = new Rectangle(backgroundBox.X + ClientSize.Width * 2 - HOLE_W * 3, HOLE_Y, HOLE_W, HOLE_H);
            //loads the image of the hole
            holeImage = Properties.Resources.hole;
        }
        //when player releases key
        private void SideScroller_KeyUp(object sender, KeyEventArgs e)
        {
            //if player releases a or d will stop player moving
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                playerVelocityX = 0;
            }

        }
        //when player presses key
        private void SideScroller_KeyDown(object sender, KeyEventArgs e)
        {
            //while game is running
            if (gameIsRunning == true)
            {
                /*
                 * LEFT AND RIGHT MOVEMENTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                 * */
                //gets he player going left
                if (e.KeyCode == Keys.A)
                {
                    playerVelocityX = -RUN_SPEED;
                    //proper drawing
                    playerDirectionImageIndex = LEFT;

                }
                //sets player going right
                if (e.KeyCode == Keys.D)
                {
                    playerVelocityX = RUN_SPEED;
                    //drawing
                    playerDirectionImageIndex = RIGHT;
                }
                /*
                 * LEFT AND RIGHT MOVEMENTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                 * */

                /*
                 * GRAVITY JUMP MOVEMENTS~###############################################################
                 * */
                //if press up or space and player is on the floor, player will start going up as jump and draws proper image
                if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Space) && playerY + PLAYER_HEIGHT == FLOOR)
                {
                    playerVelocityY = JUMP_SPEED;
                    playerDirectionImageIndex = UP;
                }
                /*
                 * GRAVITY JUMP MOVEMENTS~###############################################################
                 * */
            }
            //while the game is not running nor is at the end
            if (gameIsRunning != true && gameEnd != true)
            {
                //if press space..
                if (e.KeyCode == Keys.Space)
                {
                    //if the background is the inital start
                    if (backgroundIndex == 4)
                    {
                        //will add one so background will move to the enter information
                        backgroundIndex++;
                        //text box label and button will appear for user
                        txtEnemy.Show();
                        txtEnemy.Enabled = true;
                        txtUserName.Show();
                        txtUserName.Enabled = true;
                        btnStart.Show();
                        btnStart.Enabled = true;
                        lblUserName.Show();
                        lblNumOfEnemies.Show();
                    }
                    //if the screen isn't the userinput and is still within the starting, will add one to background index
                    else if (backgroundIndex < 7 && backgroundIndex != 5)
                    {
                        backgroundIndex++;
                    }
                    //once background index hits the end of the tutorial will set back to 4
                    else if (backgroundIndex >= 6)
                    {
                        backgroundIndex = 4;
                    }
                    //redraws everything
                    Refresh();
                }
                //short cut for qucik testing, press 5
                if (e.KeyCode == Keys.D5)
                {
                    maxEnemyNum = 5;
                    userName = "shortcut";
                    StartGame();
                }
                //h for tutorial
                if (e.KeyCode == Keys.H)
                {
                    //if presses 4 will move on to the tutorial
                    if (backgroundIndex == 4)
                    {
                        backgroundIndex = 6;
                    }
                    //if the screen isn't the userinput and is still within the starting, will add one to background index
                    else if (backgroundIndex < 7 && backgroundIndex != 5)
                    {
                        backgroundIndex++;
                    }
                    //once hits the end will reset back to 4
                    else
                    {
                        backgroundIndex = 4;
                    }
                    //redraws the screen
                    Refresh();
                }
            }
            //if end of game user can press space to play again, or press esc to close program
            if (gameEnd == true)
            {
                if (e.KeyCode == Keys.Space)
                {
                    Application.Restart();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    Application.Exit();
                }
            }
        }
        //use of starting the game
        void StartGame()
        {
            //hides and disable all the interactable ui objects
            lblUserError.Hide();
            lblUserName.Hide();
            lblNumOfEnemies.Hide();
            txtEnemy.Hide();
            txtEnemy.Enabled = false;
            txtUserName.Hide();
            txtUserName.Enabled = false;
            btnStart.Hide();
            btnStart.Enabled = false;
            //sets to the first screen (side scrolling)
            backgroundIndex = 0;
            //sets to proper elongated size for side scrooling
            backgroundBox = new Rectangle(0, 0, (ClientSize.Width * 2), (ClientSize.Height));
            //enable the timer for moving
            tmrMovements.Enabled = true;
            //sets up the enemy variables
            InitializeEnemies();
            //game is running
            gameIsRunning = true;

        }
        //if player presses the button
        private void btnStart_Click(object sender, EventArgs e)
        {
            //converts the text boxes into varialbes
            int.TryParse(txtEnemy.Text, out maxEnemyNum);
            userName = txtUserName.Text;
            //checks wheter if the inputs are good, wil start game
            if ((maxEnemyNum <= 5 && maxEnemyNum >= 3) && (userName != "" && userName != " " ))
            {
                StartGame();
            }
            //if number isn't good, will give prompt
            else if ((maxEnemyNum > 5 || maxEnemyNum < 3) && (userName != "" && userName != " "))
            {
                lblUserError.Text = "Invalid Number Of Enemies!!!";
                lblUserError.Show();
            }
            //if name isn't good, will give prompt
            else if ((userName == "" || userName == " ") && (maxEnemyNum <= 5 && maxEnemyNum >= 3))
            {
                lblUserError.Text = "Invalid Username!!!";
                lblUserError.Show();
            }
            //if name isn't good and if number isn't good, will give prompt
            else
            {
                lblUserError.Text = "Invalid Number Of Enemies and Invalid Username!!!!!";
                lblUserError.Show();
            }
        }
        //used for drawing he initial image and drawing location/size varialbe of palyer
        void InitializePlayer()
        {
            //Sets the player to the size of the window and sets it to 0,0
            // allowing it to fill the enitre background
            playerBox = new Rectangle(playerX, playerY, (PLAYER_WIDTH), (PLAYER_HEIGHT));
            playerImage[RIGHT] = Properties.Resources.kirby_idle_right_one;
            playerImage[LEFT] = Properties.Resources.kirby_idle_left;
            playerImage[UP] = Properties.Resources.kirby_up_right;
            playerImage[DOWN] = Properties.Resources.kirby_down;
        }
        #endregion
        //built in drawing 
        protected override void OnPaint(PaintEventArgs e)
        {
            //builtin function
            base.OnPaint(e);
            e.Graphics.DrawImage(backgroundImage[backgroundIndex], backgroundBox);
            //while in the game
            if (gameIsRunning == true)
            {
                //draws player according to the proper direction
                e.Graphics.DrawImage(playerImage[playerDirectionImageIndex], playerBox);
                //draws the hole to its proper location
                e.Graphics.DrawImage(holeImage, holeBox);
                //if we are in the dungeon
                if (dungeonScreen == true)
                {
                    //for every dungeon...
                    for (int i = 0; i < projectileMovement.GetLength(0); i++)
                    {
                        //for every enemy in the dungeon.. will draw the projectile
                        for (int j = 0; j < numOfEnemy[backgroundIndex - 1]; j++)
                        {
                            e.Graphics.DrawImage(projectileImage[projectileAnimationIndex[j]], projectileBox[i, j]);
                            ///resets the projectile animation index if it reaches the max amount of images
                            if (projectileAnimationIndex[j] == projectileImage.Length)
                            {
                                projectileAnimationIndex[j] = 0;
                            }
                        }
                    }
                    //for every enemy.. will draw enemy
                    for (int j = 0; j < numOfEnemy[backgroundIndex - 1]; j++)
                    {
                        e.Graphics.DrawImage(enemyImage, enemyBox[backgroundIndex - 1, j]);
                    }
                }

            }
        }
    }
}
