namespace AllBeginningsMod.Common.PrimitiveDrawing
{
    /*internal class GoodTrailStyle : ITrailStyle
    {
        public int VertexCount(int trailPositionCount) => 4 + (trailPositionCount - 2) * 3;

        public int IndexCount(int trailPositionCount) => 6 + (trailPositionCount - 2) * 12;


        public void SetBuffers(Vector2[] trailPositions, Func<float, float> trailWidth, Func<float, Color> trailColor, out List<VertexPositionColorTexture> vertices, out List<ushort> indices) {
            vertices = new();
            indices = new();

            static Vector3 ToVec3(Vector2 vector) => new(vector.X, vector.Y, 0f);

            void AddNextPositionVertices(int previousIndex, List<VertexPositionColorTexture> vertices, int trailPositionsLength) {
                int nextIndex = previousIndex + 1;

                Vector2 directionToNext = trailPositions[nextIndex] - trailPositions[previousIndex];
                float lenght = directionToNext.Length();
                Vector2 directionNormalized = directionToNext / lenght;

                Vector2 middlePosition = trailPositions[previousIndex] + directionNormalized * lenght * 0.5f;

                float nextHalfFactor = (previousIndex + 0.5f) / trailPositionsLength;
                Vector2 middlePositionOffset = directionNormalized.RotatedBy(MathHelper.PiOver2) * trailWidth(nextHalfFactor);

                vertices.Add(new VertexPositionColorTexture(ToVec3(middlePosition + middlePositionOffset), trailColor(nextHalfFactor), new Vector2(nextHalfFactor, 1f)));
                vertices.Add(new VertexPositionColorTexture(ToVec3(middlePosition - middlePositionOffset), trailColor(nextHalfFactor), new Vector2(nextHalfFactor, 0f)));

                float previousFactor = (float)previousIndex / trailPositionsLength;
                vertices.Add(new VertexPositionColorTexture(ToVec3(trailPositions[nextIndex]), trailColor(previousFactor), new Vector2(previousFactor, 0.5f)));
            }

            vertices.Add(new VertexPositionColorTexture(ToVec3(trailPositions[0]), trailColor(0f), new Vector2(0f, 0.5f)));
            AddNextPositionVertices(0, vertices, trailPositions.Length);

            indices.AddRange(
                new ushort[]
                {
                    3, 1, 0,
                    0, 2, 3
                }
            );

            for (ushort i = 1; i < trailPositions.Length - 1; i++) {
                AddNextPositionVertices(i, vertices, trailPositions.Length);
                indices.AddRange(
                    new ushort[]
                    {
                        (ushort)(i * 3 + 3), (ushort)(i * 3 + 1),(ushort)(i * 3),
                        (ushort)(i * 3), (ushort)(i * 3 + 2), (ushort)(i * 3 + 3),
                        (ushort)(i * 3), (ushort)(i * 3 + 1), (ushort)((i - 1) * 3 + 1),
                        (ushort)((i - 1) * 3 + 2), (ushort)(i * 3 + 2), (ushort)(i * 3)
                    }
                );
            }
        }
    }*/
}
