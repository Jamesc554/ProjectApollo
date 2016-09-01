using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollo
{
    [MoonSharpUserData]
    public class Camera
    {
        public Vector2 position { get; private set; }
        public float zoom { get; private set; }
        public float rotation { get; private set; }

        public int viewportWidth;
        public int viewportHeight;

        public Vector2 viewportCenter
        {
            get
            {
                return new Vector2(viewportWidth * 0.5f, viewportHeight * 0.5f);
            }
        }

        public Camera()
        {
            zoom = 1.0f;
        }

        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)position.X,
            -(int)position.Y, 0) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
            Matrix.CreateTranslation(new Vector3(viewportCenter, 0));
            }
        }

        public void AdjustZoom(float amount)
        {
            zoom += amount;
            if (zoom < 0.25f)
                zoom = 0.25f;
        }

        public void MoveCamera(Vector2 cameraMovement)
        {
            Vector2 newPos = position + cameraMovement;

            position = newPos;
        }

        //public void CenterOn(GameObject gameObject)
        //{
        //    position = CenteredPosition(gameObject);
        //}

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }

        public int WorldToScreenX(int x, int y)
        {
            Vector2 newVec2 = Vector2.Transform(new Vector2(x, y), TranslationMatrix);
            return (int)newVec2.X;
        }

        public int WorldToScreenY(int x, int y)
        {
            Vector2 newVec2 = Vector2.Transform(new Vector2(x, y), TranslationMatrix);
            return (int)newVec2.Y;
        }

        public int ScreenToWorldX(int x, int y)
        {
            Vector2 newVec2 = Vector2.Transform(new Vector2(x, y), Matrix.Invert(TranslationMatrix));
            return (int)newVec2.X;
        }

        public int ScreenToWorldY(int x, int y)
        {
            Vector2 newVec2 = Vector2.Transform(new Vector2(x, y), Matrix.Invert(TranslationMatrix));
            return (int)newVec2.Y;
        }

        public void HandleInput(InputState inputState,
      PlayerIndex? controllingPlayer)
        {
            Vector2 cameraMovement = Vector2.Zero;

            if (inputState.IsScrollLeft(controllingPlayer))
            {
                cameraMovement.X = -1;
            }
            else if (inputState.IsScrollRight(controllingPlayer))
            {
                cameraMovement.X = 1;
            }
            if (inputState.IsScrollUp(controllingPlayer))
            {
                cameraMovement.Y = -1;
            }
            else if (inputState.IsScrollDown(controllingPlayer))
            {
                cameraMovement.Y = 1;
            }
            if (inputState.IsZoomIn(controllingPlayer))
            {
                AdjustZoom(0.25f);
            }
            else if (inputState.IsZoomOut(controllingPlayer))
            {
                AdjustZoom(-0.25f);
            }

            // When using a controller, to match the thumbstick behavior,
            // we need to normalize non-zero vectors in case the user
            // is pressing a diagonal direction.
            if (cameraMovement != Vector2.Zero)
            {
                cameraMovement.Normalize();
            }

            // scale our movement to move 25 pixels per second
            cameraMovement *= 25f;

            MoveCamera(cameraMovement);
        }
    }
}
