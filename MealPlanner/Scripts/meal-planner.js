$(function () {
    $('input:radio[name=weight]').change(function () {
        console.log("weight input element changed.");
    });

    $('input:radio[name=gender]').change(function () {
        console.log("gender input element changed.");
    });

    function getUserDataInput() {
        let fname = $("#fname").val();
        let lname = $("#lname").val();
        let email = $("#email").val();
        let age = $("#age").val();
        let password = $("#password").val();
        let confirmPassword = $("#confirmpassword").val();
        var loseOrMaintainWeight = $("input[name='weight']:checked").val();
        var gender = $("input[name='gender']:checked").val();
        let heightFeet = $("#heightFeet").val();
        let heightInches = $("#heightInches").val();
        let weight = $("#weight").val();
        let activityLevel = $("#activityLevel option:selected").val();
        let weightGoal = $("#weightGoal").val();
        let daysToGoal = $("#daysToGoal").val();
        let userData = {
            "fname": fname,
            "lname": lname,
            "email": email,
            "age": age,
            "password": password,
            "confirmPassword": confirmPassword,
            "loseOrMaintainWeight": loseOrMaintainWeight,
            "gender": gender,
            "heightFeet": heightFeet,
            "heightInches": heightInches,
            "weight": weight,
            "activityLevel": activityLevel,
            "weightGoal": weightGoal,
            "daysToGoal": daysToGoal
        }
        return userData;
    }
    function validateUserPassword(password1, password2) {
        if (password1 === password2) {
            return true;
        }
        return false;
    }
    function validateEmptyFields(userData) {
        if (userData.fname === "" || userData.lname === "" || userData.email === "" || userData.password === "" || userData.confirmPassword === ""
            || userData.age === "" || userData.gender === "" || userData.heightFeet === "" || userData.heightInches === "" || userData.weight === "" || userData.activityLevel === ""
            || userData.weightGoal === "" || userData.loseOrMaintainWeight === "") {
            return true;
        }
        return false;
    }
    $("#registerButton").click(function () {
        let userData = getUserDataInput();
        if (!validateUserPassword(userData.password, userData.confirmPassword)) {
            alert("Passwords don't match.");
            return;
        }
        if (validateEmptyFields(userData)) {
            alert("Empty Fields");
            return;
        }
            $.ajax({
                url: "/User/Register",
                data: userData,
                method: "POST",
                success: function (response) {
                    if (response === undefined || response == null) {
                        console.log("Response is null");
                        return;
                    }
                    if (response.Status === 200) {
                    
                        window.location = "/Home/SignIn";

                }
                else {
                        alert("Registration Failed. Response is " + response.Status);
                }
            },
            error: function (e) {
                console.log("Registration Failed. Error: " + e);
            }
        })
    });

    $("#SignInButton").click(function () {
        let email = $("#email").val();
        let password = $("#password").val();

        if (email === "" || password === "") {
            alert("Empty Fields");
            return;
        }
        
        $.ajax({
            url: "/User/SignIn",
            data: { userid: email, password: password},
            method: "GET",
            success: function (response) {
                if (response === undefined || response == null) {
                    console.log("Response is null");
                    return;
                }
                if (response.Status === 200) {
                    
                    window.location = "/Dashboard/UserDashboard";
                }
                else {
                    alert("Incorrect UserName or Password !");
                    console.log("SignIn Failed. Response is " + response.Status);
                }
            },
            error: function (e) {
                console.log("SignIn Failed. Error: " + e);
            }
        })
    });





});