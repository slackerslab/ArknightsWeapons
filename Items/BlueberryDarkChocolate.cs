using System;
using ArknightsWeapons.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArknightsWeapons.Items
{
	public class BlueberryDarkChocolate : ModItem
	{



        public override void SetDefaults()
		{
			Item.damage = 170;
            Item.DamageType = DamageClass.Melee;
            Item.width = 66;
			Item.height = 70;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(0, 22, 50, 0);
			Item.rare = 9;
			Item.UseSound = BlueberryDarkChocolateSound;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<BlueberryDarkChocolateProjectile>();
			//Item.shoot = Mod.ProjectileType("BlueberryDarkChocolateProjectile");
			Item.shootSpeed = 2f;
		}

		SoundStyle BlueberryDarkChocolateSound = new SoundStyle("ArknightsWeapons/Sounds/BlueberryDarkChocolateSound") with
		{
			Volume = 0.5f,
			PitchVariance = 0.3f,
			MaxInstances = 0,
			//SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
		};

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BrokenHeroSword, 2);
            recipe.AddIngredient(ModContent.ItemType<ArknightsWeapons.Items.Blueberry>());
            recipe.AddIngredient(ModContent.ItemType<ArknightsWeapons.Items.DarkChocolate>());
            recipe.AddTile(TileID.MythrilAnvil);
            //recipe.AddTile(TileID.OrichalcumAnvil);
            recipe.Register();
        }

    }
}

//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace ArknightsWeapons.Items
//{
//    public class BlueberryDarkChocolate : ModItem
//    {
       
//        public override void SetDefaults()
//        {
//            Item.width = 20;
//            Item.height = 20;
//            Item.maxStack = 999;
//            Item.value = 100;
//            Item.rare = ItemRarityID.Blue;
//            // Set other Item.X values here
//        }

//        public override void AddRecipes()
//        {
//            // Recipes here. See Basic Recipe Guide
//        }
//    }
//}