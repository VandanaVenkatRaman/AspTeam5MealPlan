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
    $("#registerButton").click(function () {
        let userData = getUserDataInput();
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
                    console.log("RegistrationSuccessful");
                }
                else {
                    console.log("Registration Failed. Response is " + response.Status);
                }
            },
            error: function (e) {
                console.log("Registration Failed. Error: " + e);
            }
        })
    });
});