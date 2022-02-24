// const form = document.getElementById('form');
// const username = document.getElementById('username');
// const email = document.getElementById('email');
// const password = document.getElementById('password');
// const password2 = document.getElementById('password2');
// var send;
// form.addEventListener('submit', e => {
// 	e.preventDefault();
// 	checkInputs();
// });


//login


	$('#sub').on('click', function (e) {
		if (checkInputs()) {
			alert("send form");
			alert(username.value);
			//send form
			$.ajax({
				origin: '*',
				type: "get",
				url: '/Account/LoginForm',  //ten Action
				data: {
					username: username.value,
					password: password.value,
				},
				beforeSend: function () {
					alert("beforesend");
					/*$('.modal__contener').html('<center><div class="spinner"></div></center>');
					$('.modal_foo').html("");*/
				},
				success: function (msg) {
					alert(msg);

<<<<<<< HEAD
	var checkUserName;
	var checkEmail;
	var checkPassword;
	var checkPassword2;
	var checkVertifyCode;

	if (vertifyCode !== null) {
		var CodeLengthReg = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]{6,}$");
		const vertifyCode = document.getElementById('vertifyCode');
		const vertifyCodeValue = vertifyCode.value.trim();

		if (vertifyCodeValue === '') {
			setErrorFor(vertifyCode, 'Vertify code can\'t be blank');
			send = false;

		} else if (!vertifyCode.match(CodeLengthReg))
		{
			setErrorFor(vertifyCode, 'Vertify code must be 6 numbers');
			send = false;
		}
		else {
			checkVertifyCode = true;
		}
    }
	// trim to remove the whitespaces
	if (username !== null) {
		const usernameValue = username.value.trim();
		if (usernameValue === '') {
			setErrorFor(username, 'Username can\'t be blank');
			send = false;
=======
					/*	$('.modal__contener').html('<h2>Successfull</h2>');
						$('.modal_foo').html('<a href="dashboard"><button id="close">CLOSE</button ></a >');*/
				},
				error: function (jqXHR, textStatus, errorThrown) {
					alert(textStatus);
				}
			});
>>>>>>> 9d23ecff779d67a88eae77f433978862a8829da9
		} else {
			$('.modal_container').css('display', 'flex');

		}
	});
	function checkInputs() {
		var send = true;

		const form = document.getElementById('form');
		const username = document.getElementById('username');
		const email = document.getElementById('email');
		const password = document.getElementById('password');
		const password2 = document.getElementById('password2');
		const vertifyCode = document.getElementById('vetifyCode');

		const passwordValue = password.value.trim();

		var checkUserName;
		var checkEmail;
		var checkPassword;
		var checkPassword2;
		var checkVertifyCode;

		/*if (vertifyCode !== null) {
			var CodeLengthReg = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]{6,}$");
			const vertifyCode = document.getElementById('vertifyCode');
			const vertifyCodeValue = vertifyCode.value.trim();
	
			if (vertifyCodeValue === '') {
				setErrorFor(vertifyCode, 'Vertify code can\'t be blank');
				send = false;
	
			} else if (!vertifyCode.match(CodeLengthReg)
			{
				setErrorFor(vertifyCode, 'Vertify code must be 6 numbers');
				send = false;
			}
			else {
				checkVertifyCode = true;
			}
		}*/
		// trim to remove the whitespaces
		if (username !== null) {
			const usernameValue = username.value.trim();
			if (usernameValue === '') {
				setErrorFor(username, 'Username can\'t be blank');
				send = false;
			} else {
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
			var passwordLengthReg = new RegExp("^[ A-Za-z0-9:;,?!+-/*@]{6,}$");

			if (passwordValue === '') {
				setErrorFor(password, 'Password can\'t be blank');
				send = false;

			} else if (!passwordValue.match(passwordRegex)) {
				setErrorFor(password, 'Password must contain character in alphabets');
				send = false;
			} else if (!passwordValue.match(passwordLengthReg)) {
				setErrorFor(password, 'Password contains at least 6 characters');
				send = false;
			} else {
				checkPassword = true;
			}
		}

		if (password2 !== null) {
			const password2Value = password2.value.trim();

			if (password2Value === '') {
				setErrorFor(password2, 'Password can\'t be blank');
				send = false;

			} else if (!passwordValue.match(passwordRegex)) {
				setErrorFor(password, 'Password must contain character in alphabets');
				send = false;
			} else if (!passwordValue.match(passwordLengthReg)) {
				setErrorFor(password, 'Password contains at least 6 characters');
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
			if (checkVertifyCode) setSuccessFor(vertifyCode);
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
			setSuccessFor(email);
		}
	}

	const open = document.getElementById('open');
	const close = document.getElementById('close');
	const modal = document.getElementById('modal_container');

<<<<<<< HEAD
		if (password2Value === '') {
			setErrorFor(password2, 'Password can\'t be blank');
			send = false;

		} else if (!passwordValue.match(passwordRegex)) {
			setErrorFor(password, 'Password must contain character in alphabets');
			send = false;
		} else if (!passwordValue.match(passwordLengthReg)) {
			setErrorFor(password, 'Password contains at least 6 characters');
			send = false;
		} else if (passwordValue !== password2Value) {
			setErrorFor(password2, 'Passwords does not match');
			send = false;
		} else {
			checkPassword2 = true;
		}
	}
	if (!send){
		if(checkUserName) setSuccessFor(username);
		if(checkEmail) setSuccessFor(email);
		if(checkPassword) setSuccessFor(password);
		if (checkPassword2) setSuccessFor(password2);
		if (checkVertifyCode) setSuccessFor(vertifyCode);
		return false;
	}
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
		setSuccessFor(email);
	}
}

const open = document.getElementById('open');
const close = document.getElementById('close');
const modal = document.getElementById('modal_container');

open.addEventListener('click', (e) => {
  modal.classList.add('show');
});

close.addEventListener('click', (e) => {
  modal.classList.remove('show');
});
=======
	/*open.addEventListener('click', () => {
	  modal.classList.add('show');
	});
	
	close.addEventListener('click', () => {
	  modal.classList.remove('show');
	});*/
>>>>>>> 9d23ecff779d67a88eae77f433978862a8829da9
