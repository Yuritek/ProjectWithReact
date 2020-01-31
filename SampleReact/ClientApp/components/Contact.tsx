import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'isomorphic-fetch';

interface FetchEmployeeDataState {
	contactList: ContactData[];
	loading: boolean;
}

export class Contact extends React.Component<RouteComponentProps<{}>, FetchEmployeeDataState> {
	constructor() {
		super();
	    this.state = { contactList: [], loading: true };
	    fetch('api/Contact/Index')
			.then(response => response.json() as Promise<ContactData[]>)
			.then(data => {
			    this.setState({ contactList: data, loading: false });
			});
		this.handleDelete = this.handleDelete.bind(this);
		this.handleEdit = this.handleEdit.bind(this);
	}

	public render() {
		let contents = this.state.loading
			? <p>
				  <em>Загрука...</em>
			  </p>
		    : this.renderEmployeeTable(this.state.contactList);
		return <div>
			       <h1>Список контактов</h1>
			       <p>
				       <Link to="/addemployee">
					       <span className='glyphicon glyphicon-cog'></span> Создать контакт
				       </Link>
			       </p>
			       {contents}
		       </div>;
	}

	private handleDelete(id: number, idExt: string) {
		if (!confirm("Вы действительно хотите удалить: " + idExt))
			return;
		else {
		    fetch('api/Contact/Delete/' + id,
				{
					method: 'delete'
				}).then(data => {
				this.setState(
					{
						contactList: this.state.contactList.filter((rec) => {
							return (rec.code != id);
						})
					});
			});
		}
	}

	private handleEdit(id: number) {
		this.props.history.push("/employee/edit/" + id);
	}

	private renderEmployeeTable(empList: ContactData[]) {
		return <table className='table'>
			       <thead>
			       <tr>

				       <th>Код</th>
				       <th>Фамилия</th>
				       <th>Имя</th>
				       <th>Отчество</th>
				       <th>Полное имя</th>
				       <th>Почта</th>
				       <th>Телефон</th>
				       <th>Редактирование записи</th>
			       </tr>
			       </thead>
			       <tbody>
			       {empList.map(emp =>
				       <tr key={emp.code}>
					       <td>{emp.codeEx}</td>
					       <td>{emp.surname}</td>
					       <td>{emp.name}</td>
					       <td>{emp.patronymic}</td>
					       <td>{emp.fullName}</td>
					       <td>{emp.email}</td>
					       <td>{emp.phone}</td>
					       <td>
						       <div className="btn-toolbar">
							       <button className="btn btn-sm btn-primary" onClick={(id) => this.handleEdit(emp.code)}>Редактировать</button>
							       <button className="btn btn-sm btn-primary" onClick={(id) => this.handleDelete(emp.code,emp.codeEx)}>Удалить</button>
						       </div>
					       </td>
				       </tr>
			       )}
			       </tbody>
		       </table>;
	}
}

export class ContactData {
	code: number = 0;
	name: string = "";
	surname: string = "";
	patronymic: string = "";
	email: string = "";
	phone: string = "";
	codeEx: string = "";
	fullName: string="";

}