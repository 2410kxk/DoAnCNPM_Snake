document.getElementById("loginForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    clearMessage("loginMessage");

    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value.trim();

    if (!email || !password) {
        showMessage("loginMessage", "Vui lòng nhập đầy đủ email và mật khẩu.");
        return;
    }

    if (!isValidEmail(email)) {
        showMessage("loginMessage", "Email không đúng định dạng.");
        return;
    }

    try {
        const result = await apiRequest("/Auth/login", "POST", {
            email: email,
            password: password
        });

        if (result.token) {
            localStorage.setItem("token", result.token);
        }

        const user = result.user || {
            id: result.id || 1,
            fullName: result.fullName || "Người dùng",
            email: email
        };

        saveCurrentUser(user);

        showMessage("loginMessage", "Đăng nhập thành công.", "success");

        setTimeout(() => {
            window.location.href = "check.html";
        }, 800);

    } catch (error) {
        showMessage("loginMessage", "Đăng nhập thất bại. " + error.message);
    }
});