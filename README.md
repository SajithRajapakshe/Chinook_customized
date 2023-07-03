# Chinook

This application is unfinished. Please complete below tasks. Spend max 2 hours.
We would like to have a short written explanation of the changes you made.

1. Move data retrieval methods to separate class / classes (use dependency injection)
2. Favorite / unfavorite tracks. An automatic playlist should be created named "My favorite tracks"
3. The user's playlists should be listed in the left navbar. If a playlist is added (or modified), this should reflect in the left navbar (NavMenu.razor). Preferrably, the left menu should be refreshed without a full page reload.
4. Add tracks to a playlist (existing or new one). The dialog is already created but not yet finished.
5. Search for artist name

When creating a user account, you will see this:
"This app does not currently have a real email sender registered, see these docs for how to configure a real email sender. Normally this would be emailed: Click here to confirm your account."
After you click 'Click here to confirm your account' you should be able to login.

Please put the code in Github. Please put the original code (our code) in the master branch, put your code in a separate branch, and make a pull request to the master branch.


# Sajith

1. Completed all 5 steps except the page refresh part of the nav bar.
2. Followed the same structure without going for React based on the time availability.
3. Not very experienced in blazor, but tried my best to learn something and continue the project in same structure.
4. Sqlite db is updated and checking in with the code.
5. Nuget package references (EF Core etc.) are re-added due to some build errors in the intial stage.
6. Used Dipendency Injection as one of the design patterns.
7. Separation of Concern, Single responsibility, Inversion of Control are used when writing the backend code.
8. Added necessary mappings and registrations in program.cs
9. An issue is being identified when clicking the menu items one by one, but couldn't fix it within the time range. 
10. You can clearly see, adding tracks to new/existing playlists, Adding/Removing favourites and search functionality based on
the artist name (Please press enter after adding your search text).
11. Business layer project is just added to build the business logic. But, because of only read/write operations are there in the test,did not use it.