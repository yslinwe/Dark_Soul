using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Assets.Editor.Tools
{
    public class GetAnimationClip
    {
        [MenuItem("Tools/3D资源/CopyAnimationClip")]
        public static void AnimationClipsCopy()
        {
            Object[] go = Selection.objects;

            string Path = AssetDatabase.GetAssetPath(go[0]);

            string parentPath = GetAnimationClip.getParentPathForAsset(Path);

            for (int i = 0; i < go.Length; i++)
            {
                string fbxPath = AssetDatabase.GetAssetPath(go[i]);

                AnimCopy(fbxPath, parentPath, go[i].name.Remove(0, 4));
            }
        }


        static void AnimCopy(string fbxPath, string parentPath, string name)
        {
            Object[] objs = AssetDatabase.LoadAllAssetsAtPath(fbxPath);

            string animationPath = "";

            AnimationClipSettings setting;

            AnimationClip srcClip;//源AnimationClip

            AnimationClip newClip;//新AnimationClip

            foreach (Object o in objs)
            {
                if (o.GetType() == typeof(AnimationClip))
                {
                    srcClip = o as AnimationClip;

                    newClip = new AnimationClip();

                    newClip.name = name;//设置新clip的名字

                    if (!Directory.Exists(parentPath + @"/anim/"))

                        Directory.CreateDirectory(parentPath + @"/anim/");

                    animationPath = parentPath + @"/anim/" + newClip.name + ".anim";

                    setting = AnimationUtility.GetAnimationClipSettings(srcClip);//获取AnimationClipSettings

                    AnimationUtility.SetAnimationClipSettings(newClip, setting);//设置新clip的AnimationClipSettings

                    newClip.frameRate = srcClip.frameRate;//设置新clip的帧率

                    EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(srcClip);//获取clip的curveBinds

                    for (int i = 0; i < curveBindings.Length; i++)
                    {
                        AnimationUtility.SetEditorCurve(newClip, curveBindings[i], AnimationUtility.GetEditorCurve(srcClip, curveBindings[i]));//设置新clip的curve
                    }

                    AssetDatabase.CreateAsset(newClip, animationPath); //AssetDatabase中的路径都是相对Asset的  如果指定路径已存在asset则会被删除，然后创建新的asset

                    AssetDatabase.SaveAssets();//保存修改

                    AssetDatabase.Refresh();

                }
            }
        }
        /// <summary>
        /// 返回传入目录的父目录(相对于asset)
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public static string getParentPathForAsset(string assetPath)
        {
            string[] pathName = assetPath.Split('/');
            string parentPath = "";

            if (pathName.Length < 2 || pathName[pathName.Length - 1] == "")
            {
                Debug.Log(assetPath + @"没有父目录！");
                return parentPath;
            }

            for (int i = 0; i < pathName.Length - 1; i++)
            {

                if (i != pathName.Length - 2)
                    parentPath += pathName[i] + @"/";
                else
                    parentPath += pathName[i];
            }

            return parentPath;
        }

    }

}