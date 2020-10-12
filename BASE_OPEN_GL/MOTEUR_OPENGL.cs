
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tao.FreeGlut;
using Tao.OpenGl;



namespace BASE_OPEN_GL
{
	partial class Program
	{
		static void Initialisation_3D()
		{
			Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE |
								 Glut.GLUT_RGB | Glut.GLUT_DEPTH);
			Glut.glutInitWindowSize(800, 600);
			Glut.glutCreateWindow("BASE OPENGL");
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT0);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
		}
		//------------------------------------------------

		static void On_Changement_Taille_Fenetre(int P_Largeur, int P_Hauteur)
		{
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Gl.glViewport(0, 0, P_Largeur, P_Hauteur);
			float L_Rapport_Largeur_Hauteur =		(float)P_Largeur / (float)P_Hauteur;
			Glu.gluPerspective(60.0, L_Rapport_Largeur_Hauteur,	1.5, 100.0);
		}
		//------------------------------------------------

		static void OPENGL_Affiche_Chaine(float P_X, float P_Y, string P_Chaine )
		{
			Gl.glDisable(Gl.GL_LIGHTING);
			Gl.glRasterPos2f(P_X, P_Y); 
			int Nombre_Caracteres = P_Chaine.Length;
			for (int Index_Caractere = 0; Index_Caractere < Nombre_Caracteres; Index_Caractere++) 	{
				Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, P_Chaine[Index_Caractere]); 
			}
			Gl.glEnable(Gl.GL_LIGHTING);
		}
		//----------------------------------------------------



	}
}
