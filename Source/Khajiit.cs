using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;

namespace ESCP_Khajiit
{
    [StaticConstructorOnStartup]

    public class QuadrupedExtension: DefModExtension
    {
        public bool Quadruped = false;

        public static QuadrupedExtension Get(Def def)
        {
            return def.GetModExtension<QuadrupedExtension>();
        }
    }

    public class PawnRenderSubWorker_Rotate: PawnRenderSubWorker
    {
        public bool canRotate(Pawn pawn)
        {
            QuadrupedExtension quadrupedExtension = QuadrupedExtension.Get(pawn.def);
            if (quadrupedExtension == null)
            {
                    return false;
            }
            
            bool quadruped = quadrupedExtension.Quadruped;

            if (quadruped && pawn.Crawling)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void TransformRotation(PawnRenderNode node, PawnDrawParms parms, ref Quaternion rotation)
        {
            Pawn pawn = parms.pawn;
            if (!parms.Portrait && canRotate(pawn))
            {
                rotation = CrawlingHeadAngle(parms.facing).ToQuat();
                if (parms.flipHead)
                {
                   rotation *= 180f.ToQuat();
                }
            }
        }

        public float CrawlingHeadAngle(Rot4 rot)
        {
                switch (rot.AsInt)
                {
                    case 0:
                        return 0f;
                    case 1:
                        return 0f;
                    case 2:
                        return 0f;
                    case 3:
                        return 0f;
                    default:
                        Log.Warning("Invalid rotation: " + rot);
                        return 0f;
                }
        }
    }
}
