﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace FuseeApp
{
    public static class SimpleMeshes 
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z}
                },

                Triangles = new ushort[]
                {
                    // front face
                    0, 2, 1, 0, 3, 2,

                    // right face
                    4, 6, 5, 4, 7, 6,

                    // back face
                    8, 10, 9, 8, 11, 10,

                    // left face
                    12, 14, 13, 12, 15, 14,

                    // top face
                    16, 18, 17, 16, 19, 18,

                    // bottom face
                    20, 22, 21, 20, 23, 22

                },

                Normals = new[]
                {
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0)
                },

                UVs = new[]
                {
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0)
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f*size)
            };
        }

        public static SurfaceEffect MakeMaterial(float4 color)
        {
            return MakeEffect.FromDiffuseSpecular(
                albedoColor: color,
                emissionColor: float3.Zero,
                shininess: 25.0f,
                specularStrength: 1f);
        }

        public static Mesh CreateCylinder(float radius, float height, int segments)
        {
            float3[] verts = new float3[segments * 4 + 2];
            float3[] norms = new float3[segments * 4 + 2];
            ushort[] tris  = new ushort[segments * 4 * 3];
            float alpha = (2 * M.Pi) / segments;
            
            // CENTER VERTICES
                // Top
                verts[4 * segments]     = new float3(0, height / 2, 0);     
                norms[4 * segments]     = float3.UnitY;                     
                // Bottom
                verts[4 * segments + 1] = new float3(0, -height / 2, 0);
                norms[4 * segments + 1] = -float3.UnitY;                 

            for (int i = 1; i < segments; i++) 
            {
            // VERTICES AND NORMALS
                // Top
                verts[4 * i + 0] = new float3(radius * M.Cos(i * alpha),  height / 2, radius * M.Sin(i * alpha));
                norms[4 * i + 0] = float3.UnitY;
                // Side 1
                verts[4 * i + 1] = new float3(radius * M.Cos(i * alpha),  height / 2, radius * M.Sin(i * alpha));
                norms[4 * i + 1] = new float3(M.Cos(i * alpha), 0, M.Sin(i * alpha));
                // Side 2
                verts[4 * i + 2] = new float3(radius * M.Cos(i * alpha), -height / 2, radius * M.Sin(i * alpha));
                norms[4 * i + 2] = new float3(M.Cos(i * alpha), 0, M.Sin(i * alpha));
                // Bottom
                verts[4 * i + 3] = new float3(radius * M.Cos(i * alpha), -height / 2, radius * M.Sin(i * alpha));
                norms[4 * i + 3] = -float3.UnitY;
            // TRIANGLES
                // Top
                tris[12 * (i-1) + 0 ] = (ushort) (4 * segments + 0);
                tris[12 * (i-1) + 1 ] = (ushort) (4 *  (i-1)   + 0);
                tris[12 * (i-1) + 2 ] = (ushort) (4 *    i     + 0);
                // Side 
                tris[12 * (i-1) + 3 ] = (ushort) (4 *  (i-1)   + 2);
                tris[12 * (i-1) + 4 ] = (ushort) (4 *    i     + 2);
                tris[12 * (i-1) + 5 ] = (ushort) (4 *    i     + 1);
                // Side 2
                tris[12 * (i-1) + 6 ] = (ushort) (4 *  (i-1)   + 2);
                tris[12 * (i-1) + 7 ] = (ushort) (4 *    i     + 1);
                tris[12 * (i-1) + 8 ] = (ushort) (4 *  (i-1)   + 1);
                // Botto
                tris[12 * (i-1) + 9 ] = (ushort) (4 * segments + 1);
                tris[12 * (i-1) + 10] = (ushort) (4 *    i     + 3);
                tris[12 * (i-1) + 11] = (ushort) (4 *  (i-1)   + 3);
            }

            // Mesh together the last segment seperately in order to not duplicate the first vertices
                verts[0] = new float3(radius * M.Cos(0 * alpha),  height / 2, radius * M.Sin(0 * alpha));
                verts[1] = new float3(radius * M.Cos(0 * alpha),  height / 2, radius * M.Sin(0 * alpha));
                verts[2] = new float3(radius * M.Cos(0 * alpha), -height / 2, radius * M.Sin(0 * alpha));
                verts[3] = new float3(radius * M.Cos(0 * alpha), -height / 2, radius * M.Sin(0 * alpha));
                norms[0] = float3.UnitY;
                norms[1] = new float3(M.Cos(segments * alpha), 0, M.Sin(segments * alpha));
                norms[2] = new float3(M.Cos(segments * alpha), 0, M.Sin(segments * alpha));
                norms[3] = -float3.UnitY;
                // Top
                tris[12 * (segments - 1) + 0]  = (ushort) (4 *   segments   + 0);
                tris[12 * (segments - 1) + 1]  = (ushort) (4 * (segments-1) + 0);
                tris[12 * (segments - 1) + 2]  = (ushort) (4 *      0       + 0);
                // Side 1 
                tris[12 * (segments - 1) + 3]  = (ushort) (4 * (segments-1) + 2);
                tris[12 * (segments - 1) + 4]  = (ushort) (4 *      0       + 2);
                tris[12 * (segments - 1) + 5]  = (ushort) (4 *      0       + 1);
                // Side 2 
                tris[12 * (segments - 1) + 6]  = (ushort) (4 * (segments-1) + 2);
                tris[12 * (segments - 1) + 7]  = (ushort) (4 *      0       + 1);
                tris[12 * (segments - 1) + 8]  = (ushort) (4 * (segments-1) + 1);
                // Bottom
                tris[12 * (segments - 1) + 9]  = (ushort) (4 *  segments    + 1);
                tris[12 * (segments - 1) + 10] = (ushort) (4 *      0       + 3);
                tris[12 * (segments - 1) + 11] = (ushort) (4 * (segments-1) + 3);

            return new Mesh 
            {
                Vertices = verts, // = 'corner points'
                Normals = norms, // = 'direction' of the vertices
                Triangles = tris
            };
        }

        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }
}
