function contentLoad() {

	fetch(`/Library/LibraryManagerApi/GetProjectListByUser/${userID}`)
		.then(response => response.json())
		.then(libraryProjects => {
			const tbody = document.querySelector("#LibraryList");

			libraryProjects.forEach(libraryProject => {

				const contentTop = `
                                <div style="padding-top: 10px;" class="col-sm-4">
                                    <div class="card" style="width: 100%;">
                                    <div style="padding: 0.5rem 1rem;" class="card-body bg-success">
                                        <b>
                                        <a style="color: white;" href="/u/${userName}/lib/${libraryProject.url}">${libraryProject.name}</a>
                                        `;

				let contentMid = '';
				if (isOwner) {
					contentMid = `
                                        <button class="btn btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" ></button>
                                        <ul class="dropdown-menu w-auto">
                                        <li>

                                        <a href="#"
                                        data-bs-toggle="modal"
                                        data-bs-target="#libraryModalWindow"
                                        data-name="${libraryProject.name}"
                                        data-url="${libraryProject.url}"
                                        data-description="${libraryProject.description}"
                                        data-id="${libraryProject.id}"
                                        onclick="libraryStatus()"
                                        class="dropdown-item">Edit</a>

                                        </li>
                                        <li><a href="${libraryProject.id}" class="dropdown-item">Delete</a></li>
                                        </ul>`;
				};


				const contentBottom = `
                                        </b>

                                    </div>

                                    <ul class="list-group list-group-flush">

                                        <li class="list-group-item  bg-light">
                                        ${libraryProject.description}
                                        <br />
                                        </li>
                                    </ul>

                                </div>
                                </div>
                                 `;

				tbody.insertAdjacentHTML("beforeend", contentTop + contentMid + contentBottom);
			});
		});
}

contentLoad();