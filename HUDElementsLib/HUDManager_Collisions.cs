using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	partial class HUDManager {
		public Vector2? HandleCollisions( HUDElement element, Vector2 desiredPosition ) {
			if( element.AvoidsCollisions() ) {
				return desiredPosition;
			}

			Vector2 validatedPosition = desiredPosition;

			for( int i = 0; i < 10; i++ ) { // <- lazy
				Vector2 iter = this.HandleFirstFoundCollision( element, validatedPosition );
				if( iter == validatedPosition ) {
					return validatedPosition;
				}

				validatedPosition = iter;
			}

			return null;
		}


		private Vector2 HandleFirstFoundCollision( HUDElement element, Vector2 desiredPosition ) {
			foreach( HUDElement elem in this.Elements.Values ) {
				if( elem == element ) {
					continue;
				}
				if( !element.CollidesWith(elem) ) {
					continue;
				}

				return element.ResolveCollision( desiredPosition, elem.GetOuterDimensions().ToRectangle() );
			}

			return desiredPosition;
		}
	}
}