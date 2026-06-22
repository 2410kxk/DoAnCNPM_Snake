document.getElementById("registerForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    clearMessage("registerMessage");

    const fullName = document.getElementById("fullName").value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value.trim();
    const confirmPassword = document.getElementById("confirmPassword").value.trim();

    if (!fullName || !email || !password || !confirmPassword) {
        showMessage("registerMessage", "Vui lòng nhập đầy đủ thông tin.");
        return;
    }

    if (!isValidEmail(email)) {
        showMessage("registerMessage", "Email không đúng định dạng.");
        return;
    }

    if (password.length < 6) {
        showMessage("registerMessage", "Mật khẩu phải có ít nhất 6 ký tự.");
        return;
    }

    if (password !== confirmPassword) {
        showMessage("registerMessage", "Mật khẩu xác nhận không khớp.");
        return;
    }

    try {
        await apiRequest("/Auth/register", "POST", {
            fullName: fullName,
            email: email,
            password: password,
            role: "User"
        });

        showMessage("registerMessage", "Đăng ký thành công. Đang chuyển sang đăng nhập...", "success");

        setTimeout(() => {
            window.location.href = "login.html";
        }, 1000);

    } catch (error) {
        showMessage("registerMessage", "Đăng ký thất bại. " + error.message);
    }
});