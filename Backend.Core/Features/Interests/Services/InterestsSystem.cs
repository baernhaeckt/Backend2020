using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace Backend.Core.Features.Interests.Services
{
    public class InterestsSystem
    {
        private static readonly IDictionary<string, IDictionary<string, IList<string>>> tree =
            new Dictionary<string, IDictionary<string, IList<string>>>
            {
                {
                    "Stadt",
                    new Dictionary<string, IList<string>>
                    {
                        {"Kultur", new List<string> {"Modern", "Klassisch"}},
                        {"Spass", new List<string> {"Action", "Unterhaltung"}}
                    }
                },
                {
                    "Land",
                    new Dictionary<string, IList<string>>
                    {
                        {"Berge", new[] {"Action", "Erholung"}},
                        {"Wasser", new[] {"Action", "Erholung"}}
                    }
                }
            };

        private static readonly IDictionary<int, IList<string>> layeredInterests = new Dictionary<int, IList<string>>
        {
            {0, new[] {"Stadt", "Land"}},
            {1, new[] {"Kultur", "Spass", "Berge", "Wasser"}},
            {2, new[] {"Action", "Erholung", "Modern", "Klassisch", "Unterhaltung"}}
        };

        private InterestsSystem(ICollection<Interest> interests)
        {
            Interests = interests;

            LayerOneDone = CheckLayerDone(0, interests);
            LayerTwoDone = CheckLayerDone(1, interests);
            LayerThreeDone = CheckLayerDone(2, interests);
        }

        private bool LayerOneDone { get; }

        private bool LayerTwoDone { get; }

        private bool LayerThreeDone { get; }

        private int UnfinishedLayerIndex 
            => !LayerOneDone ? 0 : !LayerTwoDone ? 1 : !LayerThreeDone ? 2 : -1;

        public Interest NextInterestCheck 
            => UnfinishedLayerIndex == -1 || String.IsNullOrEmpty(NextInterestCheckName) ? null : new Interest {Name = NextInterestCheckName};

        private string NextInterestCheckName
        {
            get
            {
                switch (UnfinishedLayerIndex)
                {
                    case 0:
                        return GetOneNotUsedInterest(layeredInterests[UnfinishedLayerIndex]);
                    case 1:
                    {
                        var selectedInterest =
                            Interests.FirstOrDefault(i => i.Match && layeredInterests[0].Contains(i.Name));

                        return selectedInterest == null
                            ? GetOneNotUsedInterest(layeredInterests[1])
                            : GetOneNotUsedInterest(tree[selectedInterest.Name].Keys.ToList());
                    }
                    case 2:
                    {
                        var selectedInterest =
                            Interests.FirstOrDefault(i => i.Match && layeredInterests[1].Contains(i.Name));

                        return selectedInterest == null
                            ? GetOneNotUsedInterest(layeredInterests[2])
                            : GetOneNotUsedInterest(
                                tree.Values.First(kvp =>
                                    kvp.ContainsKey(selectedInterest.Name))[selectedInterest.Name]);
                    }
                    default:
                        throw new InvalidOperationException("invalid index");
                }
            }
        }

        public ICollection<Interest> Interests { get; }

        private static bool CheckLayerDone(int layerIndex, ICollection<Interest> interests)
        {
            return interests.Where(i => layeredInterests[layerIndex].Contains(i.Name)).Any(i => i.Match)
                   || interests.Count(i => layeredInterests[layerIndex].Contains(i.Name)) == layeredInterests[layerIndex].Count();
        }

        private string GetOneNotUsedInterest(IEnumerable<string> interestOptions)
        {
            var options = interestOptions.Where(i => !Interests.Any(it => it.Name.Equals(i))).ToList();
            


            return options.Count() == 0 ? string.Empty : options[new Random().Next(0, options.Count())];
        }

        public static InterestsSystem Of(ICollection<Interest> interests) => new InterestsSystem(interests);

        public static InterestsSystem Init() => Of(new List<Interest>());
    }
}