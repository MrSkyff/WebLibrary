function categoryListByLibraryLoad() {

    fetch(`/Library/CategoryManagerApi/GetCategoryListByLibrary?libraryUrl=${libraryUrl}&userId=${userId}`)
        .then(response => response.json())
        .then(categories => {
            const tbody = document.querySelector("#CategoryList");

            categories.forEach(category => {
                if (category.parentId === 0) {

                const contentTop = `
                <div style="padding-top: 10px;" class="col-sm-4">
                <div class="card" style="width: 100%;">
                  <div style="padding: 0.5rem 1rem;" class="card-body bg-success">
                    <b>
                      <a class="text-white" href="${libraryUrl}/cat/${category.url}"> ${category.name}</a>`;
                    
                    let contentEditOptions = ``;
                    if (isOwner) {
                        contentEditOptions = `
                        <button class="btn btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" ></button>
                        <ul class="dropdown-menu w-auto">
                        <li>

                        <a href="#"
                        data-bs-toggle="modal"
                        data-bs-target="#categoryModalWindow"
                        data-name="${category.name}"
                        data-url="${category.url}"
                        data-description="${category.description}"
                        data-id="${category.id}"
                        onclick="categoryUpdateForEdit(${category.parentId}, '${category.url}')"
                        class="dropdown-item">Edit</a>
                        </li>
                        <li><a href="" class="dropdown-item">Delete</a></li>
                        </ul>`;
                    }

                 const contentMid = `
                    </b>
                  </div>
                  <ul class="list-group list-group-flush">
                  <li class="list-group-item  bg-light">
                  ${category.description}
                  </li>
                  `;

                    let contentSub = '';


                    const filteredCategorie = categories.filter(categorySub => categorySub.parentId === category.id);

                    filteredCategorie.forEach(categoryFiltered => {

                        contentSub += `       
                         <li class="list-group-item  bg-light">
                           <a href="${libraryUrl}/cat/${category.url}/sub/${categoryFiltered.url}">
                           ${categoryFiltered.name}</a>`;

                        if (isOwner) {
                            contentSub += ` 
                            <button class="btn btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" ></button>
                            <ul class="dropdown-menu w-auto">
                            <li>

                            <a href="#"
                            data-bs-toggle="modal"
                            data-bs-target="#categoryModalWindow"
                            data-name="${categoryFiltered.name}"
                            data-url="${categoryFiltered.url}"
                            data-description="${categoryFiltered.description}"
                            data-id="${categoryFiltered.id}"
                            onclick="categoryUpdateForEdit(${categoryFiltered.parentId})"
                            class="dropdown-item">Edit</a>

                            </li>
                            <li><a href="" class="dropdown-item">Delete</a></li>
                            </ul>`;

                                contentSub += `
                              <br />
                             </li>                
                            `;
                        }

                    });

                    const contentAfter = '</ul>  </div>  </div>';

                    tbody.insertAdjacentHTML("beforeend", contentTop + contentEditOptions + contentMid + contentSub + contentAfter);
                }

            });
        });
}

categoryListByLibraryLoad();