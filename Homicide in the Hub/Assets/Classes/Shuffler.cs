using UnityEngine;
using System.Collections.Generic;
using System.Linq;

	public class Shuffler {
	//A Class used to randomly shuffle a list of items 

		//__Constuctor__
		public Shuffler ()
		{
		}
		
	//Methods used for shuffling an array based on Fisher-Yates Shuffle
		public void Shuffle<T>(T[] array){
			int n = array.Length;
			for (int i = 0; i < n; i++) {
				int r = i + (int)(Random.Range(0.0f,1.0f) * (n - i));
				T t = array[r];
				array[r] = array[i];
				array[i] = t;
			}
		}

	}


