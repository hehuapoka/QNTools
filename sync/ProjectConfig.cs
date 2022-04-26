using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace sync
{
    enum CACHE_TYPE { ANIMS=0,CFX,VFX}
    class ProjectConfig
    {
        private string[] cache_type_name = { "Anims", "CFX", "VFX" };

        public string PROJECT_NAME = "GBWZ";
        public string PROJECT_PATH = "D:\\GBWZ";
        public string PROJECT_MAP_DRIVE = "P:";

        public string TEX_ASSET_MAYA = "TexAssets\\MayaTex";
        public string TEX_ASSET_MATX = "TexAssets\\MaterialX";
        public string TEX_ASSET_USD = "TexAssets\\USDAssets";
        public string TEX_ASSET_TEX = "TexAssets\\Textures";

        public string RIG_ASSET_MAYA = "RigAssets\\MayaRig";
        public string RIG_ASSET_TEX = "RigAssets\\Textures";

        public string HAIR_ASSET_MAYA = "HairAssets\\MayaHair";
        public string HAIR_ASSET_TEX = "HairAssets\\Textures";
        public string HAIR_ASSET_MATX = "HairAssets\\MaterialX";

        public string SHOT_CACHE = "ShotCache";

        public string GetServerTexAssetMaya()
        {
            return Path.Combine(PROJECT_PATH,TEX_ASSET_MAYA);
        }
        public string GetLocalTexAssetMaya()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, TEX_ASSET_MAYA);
        }

        //
        public string GetServerTexAssetMatX()
        {
            return Path.Combine(PROJECT_PATH, TEX_ASSET_MATX);
        }
        public string GetLocalTexAssetMatX()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, TEX_ASSET_MATX);
        }

        //
        //
        public string GetServerTexAssetUSD()
        {
            return Path.Combine(PROJECT_PATH, TEX_ASSET_USD);
        }
        public string GetLocalTexAssetUSD()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, TEX_ASSET_USD);
        }

        //
        public string GetServerTexAssetTex()
        {
            return Path.Combine(PROJECT_PATH, TEX_ASSET_TEX);
        }
        public string GetLocalTexAssetTex()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, TEX_ASSET_TEX);
        }

        //rig 
        public string GetServerRigAssetMaya()
        {
            return Path.Combine(PROJECT_PATH, RIG_ASSET_MAYA);
        }
        public string GetLocalRigAssetMaya()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, RIG_ASSET_MAYA);
        }

        public string GetServerRigAssetTex()
        {
            return Path.Combine(PROJECT_PATH, RIG_ASSET_TEX);
        }
        public string GetLocalRigAssetTex()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, RIG_ASSET_TEX);
        }

        //hair
        public string GetServerHairAssetMaya()
        {
            return Path.Combine(PROJECT_PATH, HAIR_ASSET_MAYA);
        }
        public string GetLocalHairAssetMaya()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, HAIR_ASSET_MAYA);
        }

        public string GetServerHairAssetMatX()
        {
            return Path.Combine(PROJECT_PATH, HAIR_ASSET_MATX);
        }
        public string GetLocalHairAssetMatX()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, HAIR_ASSET_MATX);
        }

        public string GetServerHairAssetTex()
        {
            return Path.Combine(PROJECT_PATH, HAIR_ASSET_TEX);
        }
        public string GetLocalHairAssetTex()
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, HAIR_ASSET_TEX);
        }

        //shot
        public string GetServerCacheAssetDir(string sc,string shot,CACHE_TYPE cache_type)
        {
            
            return Path.Combine(PROJECT_PATH, SHOT_CACHE,sc,shot,cache_type_name[(int)cache_type]);
        }
        public string GetLocalCacheAssetDir(string sc, string shot, CACHE_TYPE cache_type)
        {
            return Path.Combine(PROJECT_MAP_DRIVE, PROJECT_NAME, SHOT_CACHE, sc, shot, cache_type_name[(int)cache_type]);
        }
    }
}
