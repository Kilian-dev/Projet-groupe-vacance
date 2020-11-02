using System;


using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{
    partial class Program
    {
        // vous pouvez mettre vos variables globales ici
        static Random Rdm = new Random();
        static float Angle_Rotation;
        static float Position_Cube_X;
        static float Position_Cube_Y;
        static float Position_Sphere_X;
        static float Position_Sphere_Y;
        static float Delta_X;
        static float Delta_Y;
        static int cpt;
        static string Le_Message;
        static string Commandes;

        static float[] Rouge = new float[4] { 0.8f, 0.2f, 0.2f, 1 };  // ce tableau représente une couleur composée de 80% de rouge, 20% de Bleu et 20% vert. La dernière valeur doit être 1
        static float[] Bleu = new float[4] { 0.2f, 0.2f, 0.8f, 1 };   // ce tableau représente une couleur composée de 20% de rouge, 20% de Bleu et 80% vert. La dernière valeur doit être 1
        static float[] Vert_Pur;
        //==========================================================
        // Cette fonction est invoquée qu'une seule fois avant que le moteur OpenGl travaille.
        // elle est utile pour initialiser des éléments globaux à l'application
        static void Initialisation_Animation()
        {
            Position_Sphere_X = Rdm.Next(-10, 10); // apparition aléatoire
            Position_Sphere_Y = Rdm.Next(-10, 10); // apparition aléatoire
            Delta_X = Rdm.Next(-1, 1) / 10f; // direction + vitesse
            Delta_Y = Rdm.Next(-1, 1) / 10f; // direction + vitesse
            Angle_Rotation = 0;
            Vert_Pur = new float[4] { 0.0f, 1.0f, 0.0f, 1 }; // vert pur

        }


        //==========================================================
        // Cette fonction est invoquée par OpenGl chaque fois que l'on demande un glutPostRedisplay();
        static void Afficher_Ma_Scene()
        {
            //.... DEBUT DE NE PAS TOUCHER
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);   // Effacer les buffer d'affichage, de profondeur et de masque
            Gl.glMatrixMode(Gl.GL_MODELVIEW);  // choisir la matrice de vue
            Gl.glLoadIdentity(); // initialiser la matrice vue en matrice identité Le repere est donc en 0,0,0
            Glu.gluLookAt(0.0, 0.0, 20.0, // La caméra est à  0,0,20 (x y z)
                                    0.0, 0.0, 0.0,   // regarde 0,0,0 (le centre)
                                    0.0, 1.0, 0.0);  // vecteur orientation  (vers le haut)

            //..... FIN DE NE PAS TOUCHER
            // c'est ici que vous pouvez coder l'affichage d'une frame


            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, Rouge); // la couleur est choisie pour tout le reste de l'affichage jusqu'à ce que l'on en change
            Gl.glPushMatrix(); // sauvegarde du repère (on est actuellement en 0,0,0
            Gl.glTranslatef(Position_Cube_X, Position_Cube_Y, 0); // déplacer le repère sur l'axe X et L'axe Y. on ne touche pas au Z
            Gl.glRotatef(Angle_Rotation, 0, 10.0f, 0); // faire tourner le repère autour de l'axe vertical
            Glut.glutSolidCube(2.0f); // afficher un cube de 2 de côté au centre du repère (qui a été déplace et tourné)
            Gl.glPopMatrix(); // restitution du repère (on revient donc en 0,0,0)


            Gl.glPushMatrix(); // sauvegarde du repère
            Gl.glTranslatef(Position_Sphere_X, Position_Sphere_Y, 0);
            // on positionne le repère en -3 (horizontal vers la gauche) 0 en vertical et 0 en Z
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, Bleu); // la couleur de dessin est maintenant bleu
                                                               //			 Gl.glTranslatef(Position_Sphere_X, Position_Sphere_Y, 0);Gl.glTranslatef(Position_Sphere_X, Position_Sphere_Y, 0);Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, Vert_Pur); // la couleur de dessin est maintenant vert_pur
            Glut.glutSolidSphere(0.5f, 20, 20);  // un sphère au centre du repère (qui a été déplacé)
            Gl.glPopMatrix(); // restitution du repère (on revient donc en 0,0,0)
            Le_Message = $"Compteur de balle : {cpt}";

            Gl.glColor3f(0.9f, 0.5f, 0.9f); // choix de la couleur 90% de rouge 50% de vert 90% bleu

            OPENGL_Affiche_Chaine(-10, -10, Le_Message); // on affiche un texte en -10,-100 (cette fonction est développée dans Moteur_OpenGl.cs)
                                                         //........NE PAS TOUCHER .......................
            Glut.glutSwapBuffers();
        }
        //=========================================================

        // cette fonction est invoquée en boucle par openGl
        static void Animation_Scene()
        {

            Position_Sphere_Y = Position_Sphere_Y + Delta_Y; // Deplacement
            if (Position_Sphere_Y > 12 || Position_Sphere_Y < -12) // limite Y avant rebond
            {
                Delta_Y = -Delta_Y;
            }

            Position_Sphere_X = Position_Sphere_X + Delta_X; // Deplacement
            if (Position_Sphere_X > 12 || Position_Sphere_X < -12) // limite X avant rebond
            {
                Delta_X = -Delta_X;
            }
            if (Position_Cube_X >= Position_Sphere_X - 1.5 && Position_Cube_X <= Position_Sphere_X + 1.5 && Position_Cube_Y >= Position_Sphere_Y - 1.5 && Position_Cube_Y <= Position_Sphere_Y + 1.5) // Hitbox du cube (marge de 1,5) (collision)
            {

                Position_Sphere_X = 0;// retour au centre
                Position_Sphere_Y = 0;// retour au centre
                Delta_X = Rdm.Next(-10, 10) / 100f; // initialise une autre direction
                Delta_Y = Rdm.Next(-10, 10) / 100f;
                cpt++;//incrementation du compteur
            }
            Angle_Rotation += 0.1f; // on modifie la 1aleur de l'angle de rotation

            if (Position_Sphere_X > 15) Position_Sphere_X = -15;
            //........NE PAS TOUCHER .......................
            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
        }

        //======================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on appuie sur une touche spéciale (flèches, Fx, ...)
        // P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on appuie sur une touche
        static void Gestion_Touches_Speciales(int P_Touche, int P_X, int P_Y)
        {
            Console.WriteLine($"Touche Spéciale : {P_Touche}. La souris est en {P_X} {P_Y}");



            if (P_Touche == 100)  // 100 est le code de la touche <-
            {
                Position_Cube_X -= 1;
            }

            if (P_Touche == 102)  // 102 est le code de la touche ->
            {
                Position_Cube_X += 1;
            }    //                                                
            if (P_Touche == 101)  // 101 est le code de la touche  haut
            {
                Position_Cube_Y += 1;
            }
            if (P_Touche == 103)  // 103 est le code de la touche bas
            {
                Position_Cube_Y -= 1;
            }

            //........NE PAS TOUCHER .......................
            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

        }

        //======================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on appuie sur une touche normale (A,Z,E, ...)
        // P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on appuie sur une touche
        static void Gestion_Clavier(byte P_Touche, int P_X, int P_Y)
        {
            Console.WriteLine($"Touche Normale : {P_Touche}. La souris est en {P_X} {P_Y}");

            if (P_Touche == 27) // 27 est la touche "Echap"
            {
                Glut.glutLeaveMainLoop();
            }



            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

        }

        //==================================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on appuie sur un bouton de la souris
        // P_Bouton contient le code du bouton (gauche ou droite), P_Etat son etat, les coordonnées de la souris quand on appuie sur un bouton sont dans P_X et P_Y

        static void Gestion_Bouton_Souris(int P_Bouton, int P_Etat, int P_X, int P_Y)
        {
            Console.WriteLine($"Bouton Souris : {P_Bouton} est {P_Etat}. La souris est en {P_X} {P_Y}");
            //Le_Message = $"Bouton Souris : {P_Bouton} est {P_Etat}. La souris est en {P_X} {P_Y}";


            // Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
        }

        //====================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on tourne la molette de la souris
        // P_Molette contient le code de la molette, P_Sens son sens de rotation, les coordonnées de la souris quand on tourne la molette sont dans P_X et P_Y

        static void Gestion_Molette(int P_Molette, int P_Sens, int P_X, int P_Y)
        {
            Console.WriteLine($"Molette Souris : {P_Molette} tourne dans le sens {P_Sens}. La souris est en {P_X} {P_Y}");
            //Le_Message = $"Molette Souris : {P_Molette} tourne dans le sens {P_Sens}. La souris est en {P_X} {P_Y}";

            // Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
        }

        //====================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on bouge la souris sans appuyer sur un bouton
        // les coordonnées de la souris ont dans P_X et P_Y
        static void Gestion_Souris_Libre(int P_X, int P_Y)
        {
            //	Console.WriteLine($"Souris libre en {P_X} {P_Y}");



            float X = (P_X / 800f) * 2.0f - 1.0f; // resolution + conversion au format opengl
            float Y = (P_Y / 600f) * 2.0f - 1.0f;
            Position_Cube_X = X * 15.0f;
            Position_Cube_Y = Y * -15.0f;




            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
        }


        //====================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on bouge la souris tout en appuyant sur un bouton
        // les coordonnées de la souris ont dans P_X et P_Y
        static void Gestion_Souris_Clique(int P_X, int P_Y)
        {


            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

        }


    }
}
