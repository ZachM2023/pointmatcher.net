namespace pointmatcher.net
{   
    public struct EuclideanTransform
    {
        public System.Numerics.Quaternion rotation;
        public System.Numerics.Vector3 translation;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public System.Numerics.Vector3 Apply(System.Numerics.Vector3 v)
        {
            return System.Numerics.Vector3.Transform(v, this.rotation) + this.translation;
        }

        public EuclideanTransform Inverse()
        {
            EuclideanTransform result;
            result.rotation = System.Numerics.Quaternion.Conjugate(this.rotation);
            result.translation = System.Numerics.Vector3.Transform(-1 * this.translation, result.rotation);
            return result;
        }

        /// p2 = r * p + t
        /// p = (p2 - t0) * r^-1
        /// p = r^-1 * p2 - t * r^-1

        /// <summary>
        /// Computes a transform that represents applying e2 then e1
        /// </summary>
        public static EuclideanTransform operator *(EuclideanTransform e1, EuclideanTransform e2)
        {
            EuclideanTransform result;
            result.rotation = e1.rotation * e2.rotation;
            result.translation = System.Numerics.Vector3.Transform(e2.translation, e1.rotation) + e1.translation;
            return result;
        }

        public static EuclideanTransform Identity
        {
            get
            {
                return new EuclideanTransform
                {
                    translation = new System.Numerics.Vector3(),
                    rotation = System.Numerics.Quaternion.Identity
                };
            }
        }
    }
}
