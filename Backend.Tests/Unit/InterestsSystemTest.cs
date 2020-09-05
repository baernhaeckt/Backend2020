using Backend.Core.Features.Interests.Services;
using Backend.Models;
using Xunit;

namespace Backend.Tests.Unit
{
    public class InterestsSystemTest
    {
        [Fact]
        public void ShouldReturnCorrectEntryLevelInterest()
        {
            var sut = InterestsSystem.Init();

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Stadt") || nextInterest.Name.Equals("Land"));
        }

        [Fact]
        public void ShouldReturnCorrectSecondLevelInterestForCity()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Stadt", Match = true }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Kultur") || nextInterest.Name.Equals("Spass"));
        }

        [Fact]
        public void ShouldReturnCorrectSecondLevelInterestForCountry()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = true }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Berge") || nextInterest.Name.Equals("Wasser"));
        }

        [Fact]
        public void ShouldReturnCorrectSecondLevelInterestForNoSelection()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = false },
                    new Interest { Name = "Stadt", Match = false }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Berge") || nextInterest.Name.Equals("Wasser") ||
                        nextInterest.Name.Equals("Kultur") || nextInterest.Name.Equals("Spass"));
        }

        [Fact]
        public void ShouldReturnCorrectSecondLevelForAlmostNotInterests()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = false },
                    new Interest { Name = "Stadt", Match = false },
                    new Interest { Name = "Kultur", Match = false },
                    new Interest { Name = "Spass", Match = false },
                    new Interest { Name = "Berge", Match = false }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Wasser"));
        }

        [Fact]
        public void ShouldReturnCorrectThirdLevelInterestForSelection110()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Stadt", Match = true },
                    new Interest { Name = "Kultur", Match = true }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Modern") || nextInterest.Name.Equals("Klassisch"));
        }

        [Fact]
        public void ShouldReturnCorrectThirdLevelInterestForSelection100()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = true },
                    new Interest { Name = "Kultur", Match = false },
                    new Interest { Name = "Wasser", Match = true }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Action") || nextInterest.Name.Equals("Erholung"));
        }

        [Fact]
        public void ShouldReturnCorrectThirdLevelInterestForSelection000()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = false },
                    new Interest { Name = "Stadt", Match = true },
                    new Interest { Name = "Kultur", Match = false },
                    new Interest { Name = "Spass", Match = true }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Action") || nextInterest.Name.Equals("Unterhaltung"));
        }

        [Fact]
        public void ShouldReturnCorrectThirdLevelInterestForSelection010()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = false },
                    new Interest { Name = "Stadt", Match = true },
                    new Interest { Name = "Kultur", Match = true }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Modern") || nextInterest.Name.Equals("Klassisch"));
        }

        [Fact]
        public void ShouldReturnCorrectThirdLevelInterestForNoSecondLevelInterests()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = false },
                    new Interest { Name = "Stadt", Match = true },
                    new Interest { Name = "Kultur", Match = false },
                    new Interest { Name = "Spass", Match = false },
                    new Interest { Name = "Berge", Match = false },
                    new Interest { Name = "Wasser", Match = false }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.True(nextInterest.Name.Equals("Action") 
                || nextInterest.Name.Equals("Erholung")
                || nextInterest.Name.Equals("Modern")
                || nextInterest.Name.Equals("Klassisch")
                || nextInterest.Name.Equals("Unterhaltung"));
        }

        [Fact]
        public void ShouldNotFailForCase_0001()
        {
            var sut = InterestsSystem.Of(new[] {
                    new Interest { Name = "Land", Match = true },
                    new Interest { Name = "Berge", Match = false },
                    new Interest { Name = "Wasser", Match = false }
                });

            var nextInterest = sut.NextInterestCheck;
            Assert.Null(nextInterest);
        }
    }
}
