using QuestionTracker.Api.Data;
using QuestionTracker.Api.Models; // Ensure this namespace matches your Entity location
using System.Collections.Generic;
using System.Linq;

public static class LeetCodeSeeder
{
    public static void SeedSql50(ApplicationDbContext db)
    {
        // Prevent duplicate seeding
        if (db.LeetCodeQuestions.Any(q => q.Category == "SQL"))
            return;

        var questions = new List<LeetCodeQuestion>
        {
            // --- SELECT ---
            new() { Title = "Recyclable and Low Fat Products", Slug = "recyclable-and-low-fat-products", LeetCodeUrl = "https://leetcode.com/problems/recyclable-and-low-fat-products/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Find Customer Referee", Slug = "find-customer-referee", LeetCodeUrl = "https://leetcode.com/problems/find-customer-referee/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Big Countries", Slug = "big-countries", LeetCodeUrl = "https://leetcode.com/problems/big-countries/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Article Views I", Slug = "article-views-i", LeetCodeUrl = "https://leetcode.com/problems/article-views-i/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Invalid Tweets", Slug = "invalid-tweets", LeetCodeUrl = "https://leetcode.com/problems/invalid-tweets/", Difficulty = "Easy", Category = "SQL", IsFree = true },

            // --- BASIC JOINS ---
            new() { Title = "Replace Employee ID With The Unique Identifier", Slug = "replace-employee-id-with-the-unique-identifier", LeetCodeUrl = "https://leetcode.com/problems/replace-employee-id-with-the-unique-identifier/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Product Sales Analysis I", Slug = "product-sales-analysis-i", LeetCodeUrl = "https://leetcode.com/problems/product-sales-analysis-i/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Customer Who Visited but Did Not Make Any Transactions", Slug = "customer-who-visited-but-did-not-make-any-transactions", LeetCodeUrl = "https://leetcode.com/problems/customer-who-visited-but-did-not-make-any-transactions/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Rising Temperature", Slug = "rising-temperature", LeetCodeUrl = "https://leetcode.com/problems/rising-temperature/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Average Time of Process per Machine", Slug = "average-time-of-process-per-machine", LeetCodeUrl = "https://leetcode.com/problems/average-time-of-process-per-machine/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Employee Bonus", Slug = "employee-bonus", LeetCodeUrl = "https://leetcode.com/problems/employee-bonus/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Students and Examinations", Slug = "students-and-examinations", LeetCodeUrl = "https://leetcode.com/problems/students-and-examinations/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Managers with at Least 5 Direct Reports", Slug = "managers-with-at-least-5-direct-reports", LeetCodeUrl = "https://leetcode.com/problems/managers-with-at-least-5-direct-reports/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Confirmation Rate", Slug = "confirmation-rate", LeetCodeUrl = "https://leetcode.com/problems/confirmation-rate/", Difficulty = "Medium", Category = "SQL", IsFree = true },

            // --- BASIC AGGREGATE FUNCTIONS ---
            new() { Title = "Not Boring Movies", Slug = "not-boring-movies", LeetCodeUrl = "https://leetcode.com/problems/not-boring-movies/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Average Selling Price", Slug = "average-selling-price", LeetCodeUrl = "https://leetcode.com/problems/average-selling-price/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Project Employees I", Slug = "project-employees-i", LeetCodeUrl = "https://leetcode.com/problems/project-employees-i/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Percentage of Users Attended a Contest", Slug = "percentage-of-users-attended-a-contest", LeetCodeUrl = "https://leetcode.com/problems/percentage-of-users-attended-a-contest/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Queries Quality and Percentage", Slug = "queries-quality-and-percentage", LeetCodeUrl = "https://leetcode.com/problems/queries-quality-and-percentage/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Monthly Transactions I", Slug = "monthly-transactions-i", LeetCodeUrl = "https://leetcode.com/problems/monthly-transactions-i/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Immediate Food Delivery II", Slug = "immediate-food-delivery-ii", LeetCodeUrl = "https://leetcode.com/problems/immediate-food-delivery-ii/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Game Play Analysis IV", Slug = "game-play-analysis-iv", LeetCodeUrl = "https://leetcode.com/problems/game-play-analysis-iv/", Difficulty = "Medium", Category = "SQL", IsFree = true },

            // --- SORTING AND GROUPING ---
            new() { Title = "Number of Unique Subjects Taught by Each Teacher", Slug = "number-of-unique-subjects-taught-by-each-teacher", LeetCodeUrl = "https://leetcode.com/problems/number-of-unique-subjects-taught-by-each-teacher/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "User Activity for the Past 30 Days I", Slug = "user-activity-for-the-past-30-days-i", LeetCodeUrl = "https://leetcode.com/problems/user-activity-for-the-past-30-days-i/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Product Sales Analysis III", Slug = "product-sales-analysis-iii", LeetCodeUrl = "https://leetcode.com/problems/product-sales-analysis-iii/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Classes More Than 5 Students", Slug = "classes-more-than-5-students", LeetCodeUrl = "https://leetcode.com/problems/classes-more-than-5-students/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Find Followers Count", Slug = "find-followers-count", LeetCodeUrl = "https://leetcode.com/problems/find-followers-count/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Biggest Single Number", Slug = "biggest-single-number", LeetCodeUrl = "https://leetcode.com/problems/biggest-single-number/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Customers Who Bought All Products", Slug = "customers-who-bought-all-products", LeetCodeUrl = "https://leetcode.com/problems/customers-who-bought-all-products/", Difficulty = "Medium", Category = "SQL", IsFree = true },

            // --- ADVANCED SELECT AND JOINS ---
            new() { Title = "The Number of Employees Which Report to Each Employee", Slug = "the-number-of-employees-which-report-to-each-employee", LeetCodeUrl = "https://leetcode.com/problems/the-number-of-employees-which-report-to-each-employee/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Primary Department for Each Employee", Slug = "primary-department-for-each-employee", LeetCodeUrl = "https://leetcode.com/problems/primary-department-for-each-employee/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Triangle Judgement", Slug = "triangle-judgement", LeetCodeUrl = "https://leetcode.com/problems/triangle-judgement/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Consecutive Numbers", Slug = "consecutive-numbers", LeetCodeUrl = "https://leetcode.com/problems/consecutive-numbers/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Product Price at a Given Date", Slug = "product-price-at-a-given-date", LeetCodeUrl = "https://leetcode.com/problems/product-price-at-a-given-date/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Last Person to Fit in the Bus", Slug = "last-person-to-fit-in-the-bus", LeetCodeUrl = "https://leetcode.com/problems/last-person-to-fit-in-the-bus/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Count Salary Categories", Slug = "count-salary-categories", LeetCodeUrl = "https://leetcode.com/problems/count-salary-categories/", Difficulty = "Medium", Category = "SQL", IsFree = true },

            // --- SUBQUERIES ---
            new() { Title = "Employees Whose Manager Left the Company", Slug = "employees-whose-manager-left-the-company", LeetCodeUrl = "https://leetcode.com/problems/employees-whose-manager-left-the-company/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Exchange Seats", Slug = "exchange-seats", LeetCodeUrl = "https://leetcode.com/problems/exchange-seats/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Movie Rating", Slug = "movie-rating", LeetCodeUrl = "https://leetcode.com/problems/movie-rating/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Restaurant Growth", Slug = "restaurant-growth", LeetCodeUrl = "https://leetcode.com/problems/restaurant-growth/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Friend Requests II: Who Has the Most Friends", Slug = "friend-requests-ii-who-has-the-most-friends", LeetCodeUrl = "https://leetcode.com/problems/friend-requests-ii-who-has-the-most-friends/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Investments in 2016", Slug = "investments-in-2016", LeetCodeUrl = "https://leetcode.com/problems/investments-in-2016/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Department Top Three Salaries", Slug = "department-top-three-salaries", LeetCodeUrl = "https://leetcode.com/problems/department-top-three-salaries/", Difficulty = "Hard", Category = "SQL", IsFree = true },

            // --- ADVANCED STRING FUNCTIONS / REGEX / CLAUSE ---
            new() { Title = "Fix Names in a Table", Slug = "fix-names-in-a-table", LeetCodeUrl = "https://leetcode.com/problems/fix-names-in-a-table/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Patients With a Condition", Slug = "patients-with-a-condition", LeetCodeUrl = "https://leetcode.com/problems/patients-with-a-condition/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Delete Duplicate Emails", Slug = "delete-duplicate-emails", LeetCodeUrl = "https://leetcode.com/problems/delete-duplicate-emails/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Second Highest Salary", Slug = "second-highest-salary", LeetCodeUrl = "https://leetcode.com/problems/second-highest-salary/", Difficulty = "Medium", Category = "SQL", IsFree = true },
            new() { Title = "Group Sold Products By The Date", Slug = "group-sold-products-by-the-date", LeetCodeUrl = "https://leetcode.com/problems/group-sold-products-by-the-date/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "List the Products Ordered in a Period", Slug = "list-the-products-ordered-in-a-period", LeetCodeUrl = "https://leetcode.com/problems/list-the-products-ordered-in-a-period/", Difficulty = "Easy", Category = "SQL", IsFree = true },
            new() { Title = "Find Users With Valid E-Mails", Slug = "find-users-with-valid-e-mails", LeetCodeUrl = "https://leetcode.com/problems/find-users-with-valid-e-mails/", Difficulty = "Easy", Category = "SQL", IsFree = true }
        };

        db.LeetCodeQuestions.AddRange(questions);
        db.SaveChanges();
    }
}