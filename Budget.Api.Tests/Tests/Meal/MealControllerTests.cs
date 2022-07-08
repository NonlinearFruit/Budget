using System;
using System.Linq;
using Budget.Api.Meal;
using Budget.Api.Tests.Utilities;
using Xunit;

namespace Budget.Api.Tests.Tests.Meal;

public class MealControllerTests : MealContextTests
{
    private readonly MealController _controller;

    protected MealControllerTests()
    {
        _controller = new MealController(_actContext);
    }

    public class GetMeals : MealControllerTests
    {
        [Fact]
        public void youngest_meals_are_returned_first()
        {
            var younger = DateTime.Now;
            var older = younger.AddDays(-1);
            _arrangeContext.Meals.Add(new() {Date = older, MealTime = "", Food = ""});
            _arrangeContext.Meals.Add(new() {Date = younger, MealTime = "", Food = ""});
            _arrangeContext.SaveChanges();

            var meals = _controller.GetMeals().Result;

            var meal = meals.Value.First();
            Assert.Equal(younger, meal.Date);
        }

        [Fact]
        public void meal_times_are_alphabetical()
        {
            var supper = "Supper";
            var breakfast = "Breakfast";
            _arrangeContext.Meals.Add(new() {MealTime = breakfast, Food = ""});
            _arrangeContext.Meals.Add(new() {MealTime = supper, Food = ""});
            _arrangeContext.SaveChanges();

            var meals = _controller.GetMeals().Result;

            var meal = meals.Value.First();
            Assert.Equal(supper, meal.MealTime);
        }
    }
}