
$(document).ready(function () {
	$('#form').on('submit', function (e) {
		if (!checkInputs()) {
			e.preventDefault();
		} else {
			$('.modal_container').css('display', 'flex');
		}
	});
});

$(document).ready(function () {
	$('#recoverPasswordForm').on('submit', function (e) {
		if (!checkGmail()) {
			e.preventDefault();
		} else {
			$('.modal_container').css('display', 'flex');
		}
	});
});

$(document).ready(function () {
	$('#comfirmForm').on('submit', function (e) {
		if (!checkVertifyCode()) {
			e.preventDefault();
		} else {
			$('.modal_container').css('display', 'flex');
		}
	});
});


function checkInputs() {
	var send = true;

	const form = document.getElementById('form');
	const username = document.getElementById('username');
	const email = document.getElementById('email');
	const password = document.getElementById('password');
	const password2 = document.getElementById('password2');


	const passwordValue = password.value.trim();

	var checkUserName;
	var checkEmail;
	var checkPassword;
	var checkPassword2;

	// trim to remove the whitespaces
	if (username !== null) {
		var regularExpression = "^[A-Za-z][A-Za-z0-9_]{7,29}$";

		const usernameValue = username.value.trim();
		if (usernameValue === '') {
			setErrorFor(username, 'Username can\'t be blank');
			send = false;
		} else if (!usernameValue.match(regularExpression)){
			setErrorFor(username, 'Username contains at 6 to 20 characters');
			send = false;
		}else {
			checkUserName = true;
		}
	}

	if (email !== null) {
		const emailValue = email.value.trim();
		if (emailValue === '') {
			setErrorFor(email, 'Email can\'t be blank');
			send = false;

		} else if (!isEmail(emailValue)) {
			setErrorFor(email, 'Not a valid email');
			send = false;

		} else {
			checkEmail = true;
		}
	}
	if (password !== null) {
		var passwordRegex = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]*$");
		var passwordLengthReg = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]{6,20}$");

		if (passwordValue === '') {
			setErrorFor(password, 'Password can\'t be blank');
			send = false;

		} else if (!passwordValue.match(passwordRegex)) {
			setErrorFor(password, 'Password must contain character in alphabets');
			send = false;
		} else if (!passwordValue.match(passwordLengthReg)) {
			setErrorFor(password, 'Password contains at 6 to 20 characters');
			send = false;
		} else {
			checkPassword = true;
		}
	}

	if (password2 !== null) {
		var passwordRegex = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]*$");
		var passwordLengthReg = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]{6,}$");
		const password2Value = password2.value.trim();

		if (password2Value === '') {
			setErrorFor(password2, 'Password can\'t be blank');
			send = false;

		} else if (!password2Value.match(passwordRegex)) {
			setErrorFor(password2, 'Password must contain character in alphabets');
			send = false;
		} else if (!password2Value.match(passwordLengthReg)) {
			setErrorFor(password2, 'Password contains at 6 to 12 characters');
			send = false;
		} else if (passwordValue !== password2Value) {
			setErrorFor(password2, 'Passwords does not match');
			send = false;
		} else {
			checkPassword2 = true;
		}
	}
	if (!send) {
		if (checkUserName) setSuccessFor(username);
		if (checkEmail) setSuccessFor(email);
		if (checkPassword) setSuccessFor(password);
		if (checkPassword2) setSuccessFor(password2);
		return false;
	} else return true;
}

function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'form-control error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'form-control success';
}

function isEmail(email) {
	return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
}

function checkGmail() {
	const email = document.getElementById('email');
	const emailValue = email.value.trim();

	if (emailValue === '') {
		setErrorFor(email, 'Email can\'t be blank');
		return false;

	} else if (!isEmail(emailValue)) {
		setErrorFor(email, 'Not a valid email');
		return false;

	} else {
		return true;
		/*setSuccessFor(email);*/
		
	}
}

function checkVertifyCode() {
	const vertifyCode = document.getElementById('vertifyCode');
	const vertifyCodeValue = vertifyCode.value.trim();

	if (vertifyCodeValue === '') {
		setErrorFor(vertifyCode, 'Vertify code can\'t be blank');
		return false;

	} else {
		return true;
	}
}

const open = document.getElementById('open');
const close = document.getElementById('close');
const modal = document.getElementById('modal_container');

/*open.addEventListener('click', () => {
	modal.classList.add('show');
});

close.addEventListener('click', () => {
	modal.classList.remove('show');
});
*/




	
