using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    internal static class EditorResources
    {
        #region Properties

        private static Texture2D _iconRecord;
        public static Texture2D IconRecord
        {
            get
            {
                if (_iconRecord == null) _iconRecord = LoadExternalTexture(@"Tools/IconRecord", @"Tools/IconRecordPro");

                return _iconRecord;
            }
        }

        private static Texture2D _iconReturn;
        public static Texture2D IconReturn
        {
            get
            {
                if (_iconReturn == null) _iconReturn = LoadExternalTexture(@"Tools/IconReturn", @"Tools/IconReturnPro");

                return _iconReturn;
            }
        }

        #endregion

        #region Private methods

        private static Texture2D LoadExternalTexture(string lightSkinFileName, string proSkinFileName = null)
        {
            if (string.IsNullOrEmpty(proSkinFileName)) proSkinFileName = lightSkinFileName;

            return Resources.Load<Texture2D>("MSS/Textures/" + (EditorGUIUtility.isProSkin ? proSkinFileName : lightSkinFileName));
        }

        private static Texture2D LoadInternalTexture(string lightSkinFileName, string proSkinFileName = null)
        {
            if (string.IsNullOrEmpty(proSkinFileName)) proSkinFileName = lightSkinFileName;

            return EditorGUIUtility.Load(EditorGUIUtility.isProSkin ? proSkinFileName : lightSkinFileName) as Texture2D;
        }

        #endregion
    }
}