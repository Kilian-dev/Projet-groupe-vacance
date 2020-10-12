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
		 static void Main(string[] args)
		{
			Glut.glutInit();
			Initialisation_3D();
			Glut.glutReshapeFunc(On_Changement_Taille_Fenetre);	
			Glut.glutDisplayFunc(Afficher_Ma_Scene);
			Glut.glutKeyboardFunc(Gestion_Clavier);
			Glut.glutSpecialFunc(Gestion_Touches_Speciales);
			Glut.glutIdleFunc(Animation_Scene);
			Glut.glutMouseFunc(Gestion_Bouton_Souris);
			Glut.glutMouseWheelFunc(Gestion_Molette);
			Glut.glutPassiveMotionFunc(Gestion_Souris_Libre);
			Glut.glutMotionFunc(Gestion_Souris_Clique);

			Initialisation_Animation();
			Glut.glutMainLoop();
		}
	}
}
 