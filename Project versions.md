A working 1.0 version of the project should have:
- [ ] A working deep learning model.
- [ ] The ability for a user to upload/choose an image and get a **numeric range** result. This can be achieved from the following sub-goals:
- Front-end
    - [ ] The user should be greeted with a welcome window that briefly describes the application and displays the current version.
    - [ ] The user should have access to a window with a button that allows them to choose an image from their PC/Phone/internet.
    - [ ] The user should be able to preview the image they chose and its filename and extension below it.
    - [ ] A button that when clicked, sends the provided image to the back-end.
    - [ ] An area that displays the **numeric range** result of the model.

- Back-end: 
    - [x] The backend should have a simple interface (API) that the front-end can easily access and send the image to.
    - [ ] The interface should pass the image to some image checking functions in the core (check if the image is grayscale or not, check the resolution, etc.)
    - [ ] Send the image to the core (image checker and adjustment functions) and expect a **numberic range** result.
    - [ ] Send the result back to the front-end to be displayed.

- Core (DL model and image adjustments):
    - [ ] Receive **ONE** image from the back-end.
    - [ ] Image processing functions should make the necessary adjustments if found.
    - [ ] Output a **numeric range** result somewhere (stdout/text file/normal variable (Assuming same language for core and back))
