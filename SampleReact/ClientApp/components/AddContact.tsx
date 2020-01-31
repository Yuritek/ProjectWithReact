import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ContactData} from './Contact';

interface AddContactDataState {
	title: string;
	loading: boolean;
	contactData: ContactData;
	emailValid: boolean;
	phoneValid: boolean;
}

export class AddContact extends React.Component<RouteComponentProps<{}>, AddContactDataState> {
	constructor(props) {
		super(props);
		this.state = { title: "", loading: true, contactData: new ContactData, emailValid: false, phoneValid: false };

		var empid = this.props.match.params["empid"];
		// This will set state for Edit employee  
		if (empid > 0) {
		    fetch('api/Contact/Details/' + empid)
				.then(response => response.json() as Promise<ContactData>)
				.then(data => {
					this.setState({ title: "Редактирование", loading: false, contactData: data, emailValid: true, phoneValid: true });
				});
		} else {
			this.state = {
				title: "Создание контакта",
				loading: false,
				contactData: new ContactData,
				emailValid: true,
				phoneValid: true
			};
		}

		this.handleSave = this.handleSave.bind(this);
		this.handleCancel = this.handleCancel.bind(this);
	}

	public render() {
		let contents = this.state.loading
			? <p>
				  <em>Загрузка...</em>
			  </p>
			: this.renderCreateForm();
		return <div>
			       <h1>{this.state.title}</h1>
			       <h3>Контрагент</h3>
			       <hr/>
			       {contents}
		       </div>;
	}

	private handleSave(event) {
		event.preventDefault();
		const data = new FormData(event.target);
		if (this.state.emailValid == null || this.state.emailValid === false) {
			alert("Укажите корректно почту: ");
			return;
		}
		if (this.state.phoneValid == null || this.state.phoneValid === false) {
			alert("Укажите корректно телефон: ");
			return;
		}
		if (this.state.contactData.code) {
		    fetch('api/Contact/Edit',
					{
						method: 'PUT',
						body: data,
					}).then((response) => response.json())
				.then((responseJson) => {
					this.props.history.push("/fetchemployee");
				})
		} else {
		    fetch('api/Contact/Create',
					{
						method: 'POST',
						body: data,
					}).then((response) => response.json())
				.then((responseJson) => {
					this.props.history.push("/fetchemployee");
				})
		}
	}

	private handleCancel(e) {
		e.preventDefault();
		this.props.history.push("/fetchemployee");
	}

	handleUserInput = (e) => {
		const name = e.target.name;
		const value = e.target.value;
		this.setState({ [name]: value },
			() => { this.validateField(name, value) });
	}

	validateField(fieldName, value) {

		let emailValid = this.state.emailValid;
		let phoneValid = this.state.phoneValid;

		switch (fieldName) {
		case 'email':
			emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
			break;
		case 'phone':
			phoneValid = value.match(/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/);
			break;
		default:
			break;
		}
		this.setState({
			phoneValid: phoneValid,
			emailValid: emailValid,

		});
	}

	errorClass(error) {
		return (error !== null && (error.length > 0 || error === true) ? '' : 'has-error');
	}

	private renderCreateForm() {
		return (
			<form onSubmit={this.handleSave}>
				<div className="form-group row">
					<input type="hidden" name="code" value={this.state.contactData.code}/>
				</div>
				<div className="form-group row">
					<label className=" control-label col-md-12" htmlFor="Surname">Фамилия</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="Surname" defaultValue={this.state.contactData.surname} required/>
					</div>
				</div >
				<div className="form-group row">
					<label className="control-label col-md-12" htmlFor="name">Имя</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="name" defaultValue={this.state.contactData.name} required/>
					</div>
				</div >
				<div className="form-group row">
					<label className="control-label col-md-12" htmlFor="patronymic">Отчество</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="patronymic" defaultValue={this.state.contactData.patronymic} required/>
					</div>
				</div>
				<div className={`form-group row ${this.errorClass(this.state.emailValid)}`}>
					<label className="control-label col-md-12" htmlFor="email">Почта</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="email" onChange={this.handleUserInput} defaultValue={this
						  .state.contactData.email}/>
					</div>
				</div>
				<div className={`form-group row ${this.errorClass(this.state.phoneValid)}`}>
					<label className="control-label col-md-12" htmlFor="phone">Телефон</label>
					<div className="col-md-4">
						<input className="form-control" type="text" name="phone" onChange={this.handleUserInput} defaultValue={this
						  .state.contactData.phone}/>
					</div>
				</div>
				<div className="btn-toolbar">
					<button className="btn btn-lg btn-primary" type="submit" name="save">Сохранить</button>
					<button className="btn btn-lg btn-primary" onClick={this.handleCancel}>Отмена</button>
				</div >
			</form >
		)
	}
}