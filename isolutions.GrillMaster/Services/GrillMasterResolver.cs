using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using isolutions.GrillMaster.Entities;

namespace isolutions.GrillMaster.Services
{
    public class GrillMasterResolver : IGrillMasterResolver
    {
        private readonly IGrillMenuService _grillMenuService;

        public GrillMasterResolver(IGrillMenuService grillMenuService)
        {
            this._grillMenuService = grillMenuService;
        }

        public async Task Resolve()
        {
            var menus = await _grillMenuService.GetGrillMenus();
            using IEnumerator<GrillMenu> allMenus = menus.GetEnumerator();
            // TODO: Implement algorithm and show results
            Griller grill = new Griller();
            int grillArea = grill.GrillArea1;
            while (allMenus.MoveNext())
            {
                var menu = allMenus.Current;
                if (menu == null) continue;
                var round = CalculateRoundsNeeded(menu, grillArea);
                Console.WriteLine($"{menu.Menu}: {round} rounds");
                List<List<GrillItem>> foodElements = SortMenuItems(round, menu.Items, grillArea);
                grill.StartRound(TimeSpan.Parse("00:08:00"), foodElements);
            }
        }

        /// <summary>
        /// This method calculate the exact number of round needed by menu
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="grillArea">Area of the grill</param>
        /// <returns>Interger (number of rounds)</returns>
        private static int CalculateRoundsNeeded(GrillMenu menu, int grillArea)
        {
            using var items = menu.Items.GetEnumerator();

            int total = 0;
            while (items.MoveNext())
            {
                var item = items.Current;
                if (item == null) continue;

                total += item.Length * item.Width * item.Quantity;
            }

            return Convert.ToInt32(Math.Ceiling((double) total / grillArea));
        }

        ///  <summary>
        /// this method organizes the line items based on the number of rounds needed
        /// 
        ///  </summary>
        ///  <param name="numOfRound">Int with the round of the grill</param>
        ///  <param name="items">All menu items to be cooked</param>
        ///  <param name="grillArea">Area of the grill to place the food</param>
        ///  <returns>nested list with each item</returns>
        private static List<List<GrillItem>> SortMenuItems(int numOfRound, IEnumerable<GrillItem> items, int grillArea)
        {
            using var allItems = items.GetEnumerator();
            List<GrillItem> individualItems = new List<GrillItem>();
            while (allItems.MoveNext())
            {
                var item = allItems.Current;
                if (item == null) continue;
                int queantity = item.Quantity;
                while (queantity > 0)
                {
                    individualItems.Add(item);
                    queantity--;
                }
            }

            return CheckIfRoundMatchTotalArea(individualItems, numOfRound, grillArea);
        }

        /// <summary>
        /// This method shuffle a 2d array (each sub array represents a round) the area of the elements can not be more than griller size
        /// </summary>
        /// <param name="individualItems">This is a representation of each item with id and size</param>
        /// <param name="numOfRound">Total rounds, to split the list</param>
        /// <param name="grillArea">Grill area/size</param>
        /// <returns></returns>
        /// 
        private static List<List<GrillItem>> CheckIfRoundMatchTotalArea(
            IList<GrillItem> individualItems, int numOfRound, int grillArea)
        {
            var splitList = SplitIntoRounds(individualItems, numOfRound);

            foreach (var sublist in splitList)
            {
                var total = 0;
                foreach (var value in sublist)
                {
                    total = value.Length * value.Width;
                }

                if (total > grillArea)
                {
                    var shuffled = individualItems.OrderBy(item => new Random().Next()).ToList();
                    CheckIfRoundMatchTotalArea(shuffled, numOfRound, grillArea);
                }
            }

            return splitList;
        }

        /// <summary>
        ///Method to split an array based on the quantity of round needed
        /// </summary>
        /// <param name="foodItems"></param>
        /// <param name="round"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<List<T>> SplitIntoRounds<T>(IList<T> foodItems, int round)
        {
            return foodItems
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index % round)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}