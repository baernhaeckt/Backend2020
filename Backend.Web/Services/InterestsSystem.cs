using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Web.Services
{
    public class InterestsSystem
    {
        static IDictionary<string, IDictionary<string, IList<string>>> tree = new Dictionary<string, IDictionary<string, IList<string>>>  {
            {
                "Stadt",
                new Dictionary<string, IList<string>>
                {
                    { "Kultur", new List<string> { "Modern", "Klassisch" } },
                    { "Spass", new List<string> { "Action", "Unterhaltung" } }
                }
            },
            {
                "Land",
                new Dictionary<string, IList<string>>
                {
                    { "Berge", new [] { "Action", "Erholung" } },
                    { "Wasser", new [] { "Action", "Erholung" } }
                }
            }
        };

        static IDictionary<int, IList<string>> layeredInterests = new Dictionary<int, IList<string>>()
        {
            { 0, new [] { "Stadt", "Land" } },
            { 1, new [] { "Kultur", "Spass", "Berge", "Wasser" } },
            { 2, new [] { "Action", "Erholung", "Modern", "Klassisch", "Unterhaltung" } }
        };

        private bool LayerOneDone { get; set; }
        private bool LayerTwoDone { get; set; }
        private bool LayerThreeDone { get; set; }

        private InterestsSystem(ICollection<Interest> interests)
        {
            Interests = interests;

            LayerOneDone = CheckLayerDone(0, interests);
            LayerTwoDone = CheckLayerDone(1, interests);
            LayerThreeDone = CheckLayerDone(2, interests);
        }

        private static bool CheckLayerDone(int layerIndex, ICollection<Interest> interests)
        {
            return interests.Where(i => layeredInterests[layerIndex].Contains(i.Name)).Any(i => i.Match)
                        || interests.Where(i => layeredInterests[layerIndex].Contains(i.Name)).Count() == layeredInterests[layerIndex].Count();
        }

        private int UnfinishedLayerIndex => !LayerOneDone ? 0 : !LayerTwoDone ? 1 : !LayerThreeDone ? 2 : -1;

        public Interest NextInterestCheck
        {
            get
            {
                if (UnfinishedLayerIndex == -1) { return null; }

                return new Interest { Name = NextInterestCheckName };
            }
        }

        private String NextInterestCheckName
        {
            get
            {
                switch (UnfinishedLayerIndex)
                {
                    case 0:
                        return GetOneNotUsedInterest(layeredInterests[UnfinishedLayerIndex]);
                    case 1:
                        {
                            var selectedInterest = Interests.FirstOrDefault(i => i.Match && layeredInterests[0].Contains(i.Name));

                            return selectedInterest == null
                                ? GetOneNotUsedInterest(layeredInterests[1])
                                : GetOneNotUsedInterest(tree[selectedInterest.Name].Keys.ToList());

                        }
                    case 2:
                        {
                            var selectedInterest = Interests.FirstOrDefault(i => i.Match && layeredInterests[1].Contains(i.Name));

                            return selectedInterest == null
                                ? GetOneNotUsedInterest(layeredInterests[2])
                                : GetOneNotUsedInterest(tree.Values.First(kvp => kvp.ContainsKey(selectedInterest.Name))[selectedInterest.Name]);
                        }
                    default:
                        throw new InvalidOperationException("invalid index");
                }
            }
        }

        private String GetOneNotUsedInterest(IList<String> interestOptions)
        {
            var options = interestOptions.Where(i => !Interests.Any(it => it.Name.Equals(i))).ToList();
            return options[new Random().Next(0, options.Count())];
        }

        public ICollection<Interest> Interests { get; }

        public static InterestsSystem Of(ICollection<Interest> interests)
        {
            return new InterestsSystem(interests);
        }

        public static InterestsSystem Init()
        {
            return Of(new List<Interest>());
        }
    }
}
